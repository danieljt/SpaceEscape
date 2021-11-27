using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A game event allows communication between objects in a scene and between scenes using a scriptable
/// object as the base class. Any Gameobject wanting to subscripe to an event can have a
/// GameEventListener component attached to it
/// </summary>
[CreateAssetMenu(menuName = "Game/GameEvent")]
public class GameEvent : ScriptableObject
{
	// Called when a gameEventListener completes a task. This is an optional method
	// for when a game event invoker wants to know about the game event listeners
	public event Action OnGameEventListenerFinishedTask;

	// This event is called when a gameEvent has been triggered. It provides a feedback
	// from the game event to the invoker(s) that the game event has been triggered.
	// It is most useful for game events with multiple invokers.
	public event Action OnGameEventTriggered;

	// These are the listeners of this event. This list is only available for
	// reading. Gae event listeners are added and removed via the GameEventListener
	// components
	private readonly List<GameEventListener> listeners = new List<GameEventListener>();

	/// <summary>
	/// Invoke the event on all listeners.
	/// </summary>
	public void Invoke()
	{
		for(int i=listeners.Count - 1; i>=0; i--)
		{
			listeners[i].Invoke();
		}

		OnGameEventTriggered?.Invoke();
	}

	/// <summary>
	/// Add a listener to the Game event
	/// </summary>
	/// <param name="newListener"></param>
	public void AddListener(GameEventListener newListener)
	{
		if (!listeners.Contains(newListener))
		{
			newListener.OnGameEventFinished += GameEventFinished;
			listeners.Add(newListener);
		}
	}

	/// <summary>
	/// Remove a listener from the Game event
	/// </summary>
	/// <param name="oldListener"></param>
	public void RemoveListener(GameEventListener oldListener)
	{
		if(listeners.Contains(oldListener))
		{
			oldListener.OnGameEventFinished -= GameEventFinished;
			listeners.Remove(oldListener);
		}
	}

	/// <summary>
	/// Gets the list containing the game event listeners
	/// </summary>
	public List<GameEventListener> Listeners
	{
		get { return listeners; }
	}

	/// <summary>
	/// This method is called when the Game event Listener has completed its task.
	/// </summary>
	protected void GameEventFinished()
	{
		OnGameEventListenerFinishedTask?.Invoke();
	}
}
