using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Is the player dead?
/// </summary>
[CreateAssetMenu(menuName ="StateMachine/Conditions/IsDead")]
public class IsDead : StateMachineCondition
{
	public override bool Evaluate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody)
	{
		if(!input.isAlive)
		{
			return true;
		}

		return false;
	}
}
