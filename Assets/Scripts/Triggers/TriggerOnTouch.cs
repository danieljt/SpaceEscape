using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script triggers an event
/// </summary>
public class TriggerOnTouch : MonoBehaviour
{
	[SerializeField] protected GameEvent gameEvent;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(gameEvent != null)
		{
			gameEvent.Invoke();
		}
		else
		{
			Debug.LogError(this + " Game event not set in inspector");
		}
	}
}
