using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is the player grounded?
/// </summary>
[CreateAssetMenu(menuName = "StateMachine/Conditions/IsGrounded")]
public class IsGrounded : StateMachineCondition
{
	public override bool Evaluate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody)
	{
		if(owner.IsGrounded)
		{
			return true;
		}

		return false;
	}
}
