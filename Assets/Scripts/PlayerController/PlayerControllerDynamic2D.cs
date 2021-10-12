using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This is a dynamic controller for the player game object. This controller uses dynamic rigidbody
/// physics. It changes the velocity of the player gameobject using velocity.
/// </summary>
public class PlayerControllerDynamic2D : MonoBehaviour
{
	[Tooltip("Speed of the character")]
	[SerializeField] protected float movementSpeed;
	[Tooltip("Maximum reachable velocity of the player in any direction")]
	[SerializeField] protected float maxMovementSpeed;
	[Tooltip("Minimum jumping height of the character")]
	[SerializeField] protected float jumpHeight;

	// Components to be used by the player
	protected Transform playerTransform;
    protected Rigidbody2D playerBody;
    protected PlayerInput playerInput;
	protected SensorController groundCheck;

	// Input system action ID's
	private Guid movementID;
	private Guid jumpID;
	private Guid fireID;

	// Action values
	private Vector2 inputVector;
	private float jump;
	private float fire;

	// Player velocity values
	private float xVelocity;
	private float yVelocity;

	// External force list 
	private List<IExternalForce> externalForces = new List<IExternalForce>(16);

	// Ground control
	private bool isGrounded;

	// Facing
	private bool isFacingRight = true;

	// Statemachine states

	private void Awake()
	{
		playerTransform = GetComponent<Transform>();
		playerBody = GetComponent<Rigidbody2D>();
		playerInput = GetComponent<PlayerInput>();
		groundCheck = GetComponentInChildren<SensorController>();

		// Get input system ids
		movementID = playerInput.currentActionMap.FindAction("move").id;
		jumpID = playerInput.currentActionMap.FindAction("jump").id;
		fireID = playerInput.currentActionMap.FindAction("fire").id;
	}

	private void OnEnable()
	{
		playerInput.onActionTriggered += HandleInput;
		groundCheck.isTouching += IsGrounded;
	}

	private void OnDisable()
	{
		playerInput.onActionTriggered -= HandleInput;
		groundCheck.isTouching -= IsGrounded;
	}

	private void FixedUpdate()
	{
		// Calculate player x velocity
		xVelocity = inputVector.x * movementSpeed;

		// Flip the character if needed
		Flip();

		// If the player is grounded, set velocity to 0. If however the player jumps,
		// calculate the jumpvelocity and jump
		if(isGrounded)
		{
			//yVelocity = 0;
			if(jump > 0)
			{
				isGrounded = false;
				yVelocity = CalculateJumpVelocity(jumpHeight);
			}
		}

		// Apply gravity to the players y velocity when the player is in the air.
		else
		{
			isGrounded = false;
			yVelocity += Physics2D.gravity.y * Time.fixedDeltaTime;
		}

		// Apply the external velocity to the player
		foreach(IExternalForce force in externalForces)
		{
			xVelocity += force.AddVelocity().x;
			yVelocity += force.AddVelocity().y;
		}

		//Debug.Log(xVelocity);
		xVelocity = Mathf.Clamp(xVelocity, -maxMovementSpeed, maxMovementSpeed);
		yVelocity = Mathf.Clamp(yVelocity, -maxMovementSpeed, maxMovementSpeed);
		playerBody.velocity = new Vector2(xVelocity, yVelocity);
		//Debug.Log(xVelocity + "      " + playerBody.velocity);
	}

	/// <summary>
	/// Calculate the jumping speed of the player given the height. This method assumes
	/// no friction, drag or jump holds. With a holding jump the height can technically be larger.
	/// </summary>
	/// <param name="height"> The jumping height</param>
	/// <returns></returns>
	private float CalculateJumpVelocity(float height)
	{
		float positiveGravity = Physics2D.gravity.y >= 0 ? Physics2D.gravity.y : -Physics2D.gravity.y;
		return Mathf.Sqrt(2*positiveGravity*height);
	}

	/// <summary>
	/// This method recieves messages from the SensorController child component.
	/// </summary>
	/// <param name="grounded"></param>
	private void IsGrounded(bool grounded)
	{
		isGrounded = grounded;
	}

	/// <summary>
	/// Flip the character
	/// </summary>
	private void Flip()
	{
		if((isFacingRight && inputVector.x < 0) || (!isFacingRight && inputVector.x > 0))
		{
			playerTransform.Rotate(0, 180, 0);
			isFacingRight = !isFacingRight;
		}
	}

	/// <summary>
	/// This method handles the input of the player through the input system so that it can be used
	/// by the controller.
	/// </summary>
	/// <param name="context"></param>
	private void HandleInput(InputAction.CallbackContext context)
	{
		if(context.action.id == movementID)
		{
			inputVector = context.ReadValue<Vector2>();
		}

		if(context.action.id == jumpID)
		{
			jump = context.ReadValue<float>();
		}

		if(context.action.id == fireID)
		{
			fire = context.ReadValue<float>();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		IExternalForce force = collision.GetComponent<IExternalForce>();
		if(force != null && !externalForces.Contains(force))
		{
			externalForces.Add(force);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		IExternalForce force = collision.GetComponent<IExternalForce>();
		if (force != null && externalForces.Contains(force))
		{
			externalForces.Remove(force);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		IExternalForce force = collision.gameObject.GetComponent<IExternalForce>();
		if (force != null && !externalForces.Contains(force))
		{
			externalForces.Add(force);
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		IExternalForce force = collision.gameObject.GetComponent<IExternalForce>();
		if (force != null && externalForces.Contains(force))
		{
			externalForces.Remove(force);
		}
	}
}
