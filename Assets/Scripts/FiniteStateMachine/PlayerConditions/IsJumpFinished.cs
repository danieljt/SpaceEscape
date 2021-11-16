using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is the player falling
/// </summary>
[CreateAssetMenu(menuName = "StateMachine/Conditions/IsFalling")]
public class IsJumpFinished : StateMachineCondition
{
	public override bool Evaluate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody)
	{
		if(input.jumpCompleted || input.jumpReleased)
		{
			return true;
		}

		return false;
	}
}
