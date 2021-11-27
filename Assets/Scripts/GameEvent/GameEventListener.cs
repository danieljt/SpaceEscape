using System;
using UnityEngine;

/// <summary>
/// A Game Event listener recieves information from an event an fires a response to that
/// event. This component is used to recieve events from other game objects in the scene
/// without having to implement event managers or singletons.
/// </summary>
public class GameEventListener : MonoBehaviour
{
    [Tooltip("The Game Event this component subscribes to")]
    public GameEvent gameEvent;

	// Called when the game event is fired
    public event Action OnGameEventTriggered;

	// Called when the object responding to the game event has finished it's task, 
	public event Action OnGameEventFinished;

    /// <summary>
    /// Called when the game event is triggered. This method just invokes the OnGameEventTriggered
	/// action. Any functionality of the event on the gameobject can be subscribed to by this action.
    /// </summary>
    public void Invoke()
	{
        OnGameEventTriggered?.Invoke();
	}

	/// <summary>
	/// Called when the listener to this game event has finished it's task.
	/// This method is optional, but allows a type of two way communication
	/// between the game event invoker and the game event listener. The gameObject invoking
	/// the event can subscribe to the OnGameEventFinished action.
	/// </summary>
	public void Finished()
	{
		OnGameEventFinished?.Invoke();
	}

	private void OnEnable()
	{
		if(gameEvent != null)
		{
			gameEvent.AddListener(this);
		}
	}

	private void OnDisable()
	{
		if(gameEvent != null)
		{
			gameEvent.RemoveListener(this);
		}
	}
}
