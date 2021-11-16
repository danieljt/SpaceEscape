using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateMachineTransition
{
	public List<StateMachineCondition> conditions;
	public StateMachineState trueState;
	public StateMachineState falseState;

	public bool Evaluate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody)
	{
		if(conditions != null)
		{
			foreach(StateMachineCondition condition in conditions)
			{
				if(!condition.Evaluate(owner, input, rbody))
				{
					return false;
				}
			}
		}
		return true;
	}
}
