using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is the player jumping?
/// </summary>
[CreateAssetMenu(menuName ="StateMachine/Conditions/IsJumping")]
public class IsJumping : StateMachineCondition
{
	public override bool Evaluate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody)
	{
		if(owner.IsGrounded && input.jumpPressed)
		{
			return true;
		}

		return false;
	}
}
