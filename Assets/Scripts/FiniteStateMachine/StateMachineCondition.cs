using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineCondition : ScriptableObject
{
	public abstract bool Evaluate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody);
}
