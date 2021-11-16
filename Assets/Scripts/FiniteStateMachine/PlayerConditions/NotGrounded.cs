using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is the player NOT grounded. Returns true if the players ground
/// check is false and the player speed is below the minimum allowed
/// fall speed.
/// </summary>
[CreateAssetMenu(menuName = "StateMachine/Conditions/NotGrounded")]
public class NotGrounded : StateMachineCondition
{
	public override bool Evaluate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody)
	{
		if(!owner.IsGrounded && rbody.velocity.y < owner.MinimumSpeedBeforeAllowedJump)
		{
			return true;
		}

		return false;
	}
}
