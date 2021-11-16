using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerTest : MonoBehaviour
{
	[SerializeField] protected float movementSpeed;
	[SerializeField] protected float airAcceleration;
	[SerializeField] protected float jumpSpeed;
	[SerializeField] protected Transform groundControl;
	[SerializeField] protected LayerMask whatIsGround;

	// Components
	protected Rigidbody2D rb2d;
	protected PlayerInput playerInput;

	// Input actions
	protected InputAction movementAction;
	protected InputAction jumpAction;
	protected Guid movementID;

	// Jump booleans
	protected bool isJumpPressed;
	protected bool isJumpReleased;
	protected bool isJumpCompleted;
	protected bool isJumping;

	// Movement
	protected Vector2 movementInput;
	protected float yVelocity;
	protected float xVelocity;
	protected int jumps = 2;
	protected int jumpCounter;

	public void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		playerInput = GetComponent<PlayerInput>();
		movementAction = playerInput.actions.FindAction("move");
		movementID = movementAction.id;
		jumpAction = playerInput.actions.FindAction("jump");
	}

	public void OnEnable()
	{
		jumpAction.started += JumpStarted;
		jumpAction.performed += JumpCompleted;
		jumpAction.canceled += JumpReleased;
		playerInput.onActionTriggered += MovementInput;
	}

	private void OnDisable()
	{
		jumpAction.started -= JumpStarted;
		jumpAction.performed -= JumpCompleted;
		jumpAction.canceled -= JumpReleased;
		playerInput.onActionTriggered -= MovementInput;
	}

	private void JumpStarted(InputAction.CallbackContext context)
	{
		isJumpPressed = true;
		isJumpCompleted = false;
		isJumpReleased = false;
	}

	private void JumpCompleted(InputAction.CallbackContext context)
	{
		isJumpPressed = false;
		isJumpCompleted = true;
		isJumpReleased = false;
	}

	private void JumpReleased(InputAction.CallbackContext context)
	{
		isJumpPressed = false;
		isJumpCompleted = false;
		isJumpReleased = true;
	}

	protected void MovementInput(InputAction.CallbackContext context)
	{
		if(context.action.id == movementID)
		{
			movementInput = context.ReadValue<Vector2>();
		}
	}

	private void FixedUpdate()
	{
		// Check ground
		bool hit = Physics2D.BoxCast(groundControl.position, new Vector2(0.6f,0.05f), 0, -transform.up, 0.02f, whatIsGround);

		// Player is on the ground
		if (hit)
		{
			yVelocity = 0;
			xVelocity = movementInput.x*movementSpeed;
			jumpCounter = jumps;

			// Player is pressing jump
			if (isJumpPressed)
			{
				jumpCounter--;
				yVelocity = jumpSpeed;
				isJumping = true;
			}
		}

		// Player is in the air
		else
		{
			xVelocity += movementInput.x * airAcceleration * Time.fixedDeltaTime;
			xVelocity = Mathf.Clamp(xVelocity, -movementSpeed, movementSpeed);

			// If the player is jumping, check if the player has completed inputs or if the
			// jump button has been released
			if(isJumping)
			{
				if(isJumpCompleted || isJumpReleased)
				{
					isJumping = false;
				}
				else
				{
					yVelocity = jumpSpeed;
				}
			}

			// The player is falling down
			else
			{
				if (isJumpPressed && jumpCounter > 0)
				{
					jumpCounter--;
					isJumping = true;
					yVelocity = jumpSpeed;
				}

				else
				{
					isJumping = false;
					yVelocity += Physics2D.gravity.y * rb2d.gravityScale * Time.fixedDeltaTime;
				}
			}
		}

		// Update velocity
		rb2d.velocity = new Vector2(xVelocity, yVelocity);
	}
}
