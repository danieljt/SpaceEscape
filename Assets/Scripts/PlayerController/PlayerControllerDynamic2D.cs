using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This is a dynamic controller for the player game object. This controller uses dynamic rigidbody
/// physics. It changes the velocity of the player gameobject using velocity.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Health))]
public class PlayerControllerDynamic2D : MonoBehaviour
{
	[Tooltip("The game manager this player communicates with")]
	public GameManager gameManager;
	[Tooltip("Maximum move speed of player")]
	public float movementSpeed;
	[Tooltip("Air acceleration of player")]
	public float airAcceleration;
	[Tooltip("The deacceleration of the player when no move input moving while in the air")]
	public float airDeacceleration;
	[Tooltip("The jump speed of the player")]
	public float jumpSpeed;
	[Tooltip("The minimum speed before the player cant ground jump")]
	public float minimumSpeedBeforeAllowedJump;
	[Tooltip("The total number of air jumps allowed by the player")]
	[SerializeField] protected int numberOfAirJumps;

	[Tooltip("Audio for walking")]
	public AudioClip walkClip;
	[Tooltip("Audio for jumping")]
	public AudioClip jumpClip;
	[Tooltip("Audio for air jumping")]
	public AudioClip airJumpClip;
	[Tooltip("Audio for dying")]
	public AudioClip deathClip;

	[Tooltip("The current state of the player")]
	public StateMachineState currentState;
	
	protected StateMachineState startingState;

	// Internal variables
	protected int jumpCounter = 0;

	// Components to be used by the player
	protected AudioSource audioSource;
    protected Rigidbody2D playerBody;
    protected PlayerInput playerInput;
	protected Animator animator;
	protected SensorController groundCheck;
	protected Health health;

	// Input actions
	protected InputAction movementAction;
	protected InputAction jumppAction;
	protected InputAction pauseAction;
	protected Guid movementID;
	protected Guid pauseID;

	// Action values
	private Vector2 inputVector;

	// The input context for this player
	protected InputContext inputContext;

	// Animator Hash values
	protected int horizontalSpeedHash = Animator.StringToHash("horizontalSpeed");
	protected int verticalSpeedHash = Animator.StringToHash("verticalSpeed");
	protected int isGroundedHash = Animator.StringToHash("isGrounded");
	protected int isAliveHash = Animator.StringToHash("isAlive");
	protected int jumpPressedHash = Animator.StringToHash("isJumpPressed");
	protected int jumpCompletedHash = Animator.StringToHash("isJumpCompleted");
	protected int jumpCancelledHash = Animator.StringToHash("isJumpReleased");

	// Ground control
	private bool isGrounded;

	// Is the player alive?
	private bool isAlive = true;

	// Facing
	private bool isFacingRight = true;

	#region Unity messages
	private void Awake()
	{
		playerBody = GetComponent<Rigidbody2D>();
		playerInput = GetComponent<PlayerInput>();
		animator = GetComponentInChildren<Animator>();
		audioSource = GetComponent<AudioSource>();
		health = GetComponent<Health>();
		groundCheck = GetComponentInChildren<SensorController>();

		// Get Player input actions
		movementAction = playerInput.currentActionMap.FindAction("move");
		jumppAction = playerInput.currentActionMap.FindAction("jump");
		pauseAction = playerInput.currentActionMap.FindAction("Pause");
		movementID = movementAction.id;
		pauseID = pauseAction.id;

		inputContext.isAliveHash = isAliveHash;
		inputContext.horizontalSpeedHash = horizontalSpeedHash;
		inputContext.verticalSpeedHash = verticalSpeedHash;
		inputContext.isGroundedHash = isGroundedHash;
		inputContext.jumpPressedHash = jumpPressedHash;
		inputContext.jumpCompletedHash = jumpCompletedHash;
		inputContext.jumpCancelledHash = jumpCancelledHash;

		startingState = currentState;
	}

	private void OnEnable()
	{
		playerInput.onActionTriggered += Move;
		playerInput.onActionTriggered += Pause;
		jumppAction.started += StartJump;
		jumppAction.performed += PerformJump;
		jumppAction.canceled += CancelJump;
		groundCheck.isTouching += Grounded;
		health.OnHealthZero += Die;
		currentState = startingState;
		inputContext.isAlive = true;
		jumpCounter = numberOfAirJumps;
	}

	private void OnDisable()
	{
		playerInput.onActionTriggered -= Move;
		playerInput.onActionTriggered -= Pause;
		jumppAction.started -= StartJump;
		jumppAction.performed -= PerformJump;
		jumppAction.canceled -= CancelJump;
		groundCheck.isTouching -= Grounded;
		health.OnHealthZero -= Die;
	}

	private void FixedUpdate()
	{
		currentState.EvaluateTransitions(this, inputContext, playerBody);
		currentState.StateFixedUpdate(this, inputContext, playerBody, animator);
	}
	#endregion

	#region input functions
	public void Move(InputAction.CallbackContext context)
	{
		if (context.action.id == movementID)
		{
			inputVector = context.ReadValue<Vector2>();
		}

		inputContext.movementInput = inputVector;
	}

	public void StartJump(InputAction.CallbackContext context)
	{
		inputContext.jumpPressed = true;
		inputContext.jumpCompleted = false;
		inputContext.jumpReleased = false;
	}

	public void PerformJump(InputAction.CallbackContext context)
	{
		inputContext.jumpPressed = false;
		inputContext.jumpCompleted = true;
		inputContext.jumpReleased = false;
	}

	public void CancelJump(InputAction.CallbackContext context)
	{
		inputContext.jumpPressed = false;
		inputContext.jumpCompleted = false;
		inputContext.jumpReleased = true;
	}

	public void Pause(InputAction.CallbackContext context)
	{
		if(context.action.id == pauseID)
		{
			inputContext.paused = !inputContext.paused;
		}
	}

	#endregion

	/// <summary>
	/// The player dies
	/// </summary>
	protected void Die()
	{
		isAlive = false;
		inputContext.isAlive = isAlive;
	}

	/// <summary>
	/// This method recieves messages from the SensorController child component.
	/// </summary>
	/// <param name="grounded"></param>
	private void Grounded(bool grounded)
	{
		isGrounded = grounded;
	}

	/// <summary>
	/// Flip the character
	/// </summary>
	public void Flip()
	{
		if((isFacingRight && inputVector.x < 0) || (!isFacingRight && inputVector.x > 0))
		{
			transform.Rotate(0, 180, 0);
			isFacingRight = !isFacingRight;
		}
	}

	/// <summary>
	/// Transition to the new state
	/// </summary>
	/// <param name="newState"></param>
	public void TransitionTo(StateMachineState newState)
	{
		if(currentState != null && newState != null)
		{
			if ((currentState == newState && currentState.allowTransitionToSelf) || (currentState != newState))
			{
				currentState.StateExit(this, inputContext, playerBody, animator);
				currentState = newState;
				newState.StateEnter(this, inputContext, playerBody, animator);
			}
		}
	}

	#region Get Set methods
	/// <summary>
	/// Is the player grounded
	/// </summary>
	public bool IsGrounded
	{
		get { return isGrounded; }
	}

	/// <summary>
	/// The number of allowed air jumps
	/// </summary>
	public int NumberOfAirJumps
	{
		get { return numberOfAirJumps; }
	}

	/// <summary>
	/// Use this to count the number of jumps the player has left
	/// </summary>
	public int JumpCounter
	{
		get { return jumpCounter; }
		set { jumpCounter = value; }
	}

	public AudioSource GetAudioSource
	{
		get { return audioSource; }
	}
	#endregion
}

#region Helper structs
/// <summary>
/// Helper struct containing useful information about the players input and animator
/// hashes from the input system and animator component.
/// </summary>
public struct InputContext
{
	public Vector2 movementInput;
	public bool jumpPressed;
	public bool jumpCompleted;
	public bool jumpReleased;
	public bool paused;
	public bool isAlive;

	public int isAliveHash;
	public int horizontalSpeedHash;
	public int verticalSpeedHash;
	public int jumpPressedHash;
	public int jumpCompletedHash;
	public int jumpCancelledHash;
	public int isGroundedHash;
}
#endregion
