using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/JumpLocomotionState")]
public class PlayerJumpLocomotionState : StateMachineState
{
	protected float xVelocity;
	protected float yVelocity;

	public override void StateEnter(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{
		xVelocity = rbody.velocity.x;

		if(Mathf.Approximately(input.movementInput.x, 0))
		{
			xVelocity *= owner.airDeacceleration;
		}

		else
		{
			xVelocity += input.movementInput.x * owner.airAcceleration * Time.fixedDeltaTime;
		}

		xVelocity = Mathf.Clamp(xVelocity, -owner.movementSpeed, owner.movementSpeed);
		yVelocity = owner.jumpSpeed;
		rbody.velocity = new Vector2(xVelocity, yVelocity);

		animator.SetBool(input.jumpPressedHash, true);
		animator.SetBool(input.jumpCompletedHash, false);
		animator.SetBool(input.jumpCancelledHash, false);
		animator.SetBool(input.isGroundedHash, true);
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
		owner.Flip();
	}

	public override void StateUpdate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{

	}
}
