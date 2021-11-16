using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/FallLocomotionState")]
public class PlayerFallLocomotionState : StateMachineState
{
	protected float xVelocity;
	protected float yVelocity;

	public override void StateEnter(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{
		xVelocity = rbody.velocity.x;

		if (Mathf.Approximately(input.movementInput.x, 0))
		{
			xVelocity *= owner.airDeacceleration;
		}

		else
		{
			xVelocity += input.movementInput.x * owner.airAcceleration * Time.fixedDeltaTime;
		}

		xVelocity = Mathf.Clamp(xVelocity, -owner.movementSpeed, owner.movementSpeed);
		yVelocity = rbody.velocity.y;
		rbody.velocity = new Vector2(xVelocity, yVelocity);

		animator.SetBool(input.jumpPressedHash, false);
		animator.SetBool(input.jumpCompletedHash, true);
		animator.SetBool(input.jumpCancelledHash, true);
		animator.SetBool(input.isGroundedHash, false);

	}

	public override void StateExit(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{

	}

	public override void StateFixedUpdate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{

		if (Mathf.Approximately(input.movementInput.x, 0))
		{
			xVelocity *= owner.airDeacceleration;
		}

		else
		{
			xVelocity += input.movementInput.x * owner.airAcceleration * Time.fixedDeltaTime;
		}

		xVelocity = Mathf.Clamp(xVelocity, -owner.movementSpeed, owner.movementSpeed);
		yVelocity = rbody.velocity.y;
		rbody.velocity = new Vector2(xVelocity, yVelocity);
		owner.Flip();
	}

	public override void StateUpdate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{

	}
}
