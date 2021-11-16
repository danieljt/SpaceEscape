using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is the player air jumping
/// </summary>
[CreateAssetMenu(menuName = "StateMachine/Conditions/IsAirJump")]
public class IsAirJump : StateMachineCondition
{
	public override bool Evaluate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody)
	{
		if(owner.JumpCounter > 0 && input.jumpPressed)
		{
			return true;
		}

		return false;
	}
}
