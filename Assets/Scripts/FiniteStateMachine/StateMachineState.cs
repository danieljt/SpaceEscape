using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// An abstract state in a state machine. A statemachine consists of an owner T, also called the context. 
/// The owner T owns a certain amount of states that are inherited from this base class, and should be created before run time. 
/// States can be added dynamically, but it is advised to have the statemachine set up beforehand.
/// </summary>
public abstract class StateMachineState : ScriptableObject
{
	[Tooltip("Can this state transition to itself?")]
	public bool allowTransitionToSelf;
	[SerializeField] protected List<StateMachineTransition> transitions;

	public abstract void StateEnter(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator);
	public abstract void StateUpdate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator);
	public abstract void StateFixedUpdate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator);
	public abstract void StateExit(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator);

	public void EvaluateTransitions(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody)
	{
		if (transitions != null)
		{
			foreach (StateMachineTransition transition in transitions)
			{
				if (transition.Evaluate(owner, input, rbody))
				{
					owner.TransitionTo(transition.trueState);
				}
			}
		}
	}
}
