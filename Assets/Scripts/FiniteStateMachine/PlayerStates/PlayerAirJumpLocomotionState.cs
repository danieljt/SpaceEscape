using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/AirJumpLocomotionState")]
public class PlayerAirJumpLocomotionState : StateMachineState
{
	protected float xVelocity;
	protected float yVelocity;

	public override void StateEnter(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{
		owner.JumpCounter--;
		yVelocity = owner.jumpSpeed;
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
		rbody.velocity = new Vector2(xVelocity, yVelocity);

		animator.SetBool(input.jumpPressedHash, true);
		animator.SetBool(input.jumpCompletedHash, false);
		animator.SetBool(input.jumpCancelledHash, false);
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
		rbody.velocity = new Vector2(xVelocity, yVelocity);
	}

	public override void StateUpdate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{

	}
}
