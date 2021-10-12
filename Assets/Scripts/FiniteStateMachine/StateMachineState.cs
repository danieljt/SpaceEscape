/// <summary>
/// An abstract state in a state machine. A statemachine consists of an owner T, also called the context. 
/// The owner T owns a certain amount of states that are inherited from this base class, and should be created before run time. 
/// States can be added dynamically, but it is advised to have the statemachine set up beforehand.
/// 
/// A statemachine state consists of the following 4 primary methods.
/// OnStateEnter(T owner)      : Run when the owner T enters the state
/// OnStateUpdate(T owner)     : Run every update frame
/// OnStateFixedUpdate(T owner): Run every fixed update frame
/// OnStateExit(T owner)       : Run when the owner T exits the state
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class StateMachineState<T>
{
	public abstract void OnStateEnter(T owner);
	public abstract void OnStateUpdate(T owner);
	public abstract void OnStateFixedUpdate(T owner);
	public abstract void OnStateExit(T owner);
}
