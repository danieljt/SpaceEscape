using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/GroundLocomotionState")]
public class PlayerGroundLocomotionState : StateMachineState
{
	protected float xVelocity;
	protected float yVelocity;

	public override void StateEnter(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{
		owner.JumpCounter = owner.NumberOfAirJumps;
		animator.SetBool(input.isGroundedHash, true);
		animator.SetFloat(input.horizontalSpeedHash, Mathf.Abs(xVelocity));
	}

	public override void StateExit(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{
		if (owner.GetAudioSource.isPlaying)
		{
			owner.GetAudioSource.Stop();
		}
	}

	public override void StateFixedUpdate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{
		xVelocity = input.movementInput.x * owner.movementSpeed;
		yVelocity = rbody.velocity.y;

		if(Mathf.Pow(xVelocity, 2) > 0.001f)
		{
			if(!owner.GetAudioSource.isPlaying)
			{
				owner.GetAudioSource.clip = owner.walkClip;
				owner.GetAudioSource.Play();
			}
		}
		else
		{
			if(owner.GetAudioSource.isPlaying)
			{
				owner.GetAudioSource.Stop();
			}
		}

		rbody.velocity = new Vector2(xVelocity, yVelocity);
		owner.Flip();
		animator.SetFloat(input.horizontalSpeedHash, Mathf.Abs(xVelocity));
	}

	public override void StateUpdate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{

	}
}
