using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script triggers an event
/// </summary>
public class TriggerOnTouch : MonoBehaviour
{
	[SerializeField] protected GameEvent gameEvent;
	public event Action OnTriggered;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(gameEvent != null)
		{
			gameEvent.Invoke();
			OnTriggered?.Invoke();
		}
		else
		{
			Debug.LogError(this + " Game event not set in inspector");
		}
	}
}
