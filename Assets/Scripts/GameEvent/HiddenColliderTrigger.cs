using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains a hidden collider that can be activated by other scripts.
/// </summary>
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(GameEventListener))]
public class HiddenColliderTrigger : MonoBehaviour
{
	[Tooltip("Called when this collider is interacted with")]
	[SerializeField] protected GameEvent gameEvent;
	[Tooltip("If this trigger should be interacted with once")]
	[SerializeField] protected bool oneShot;

	protected GameEventListener listener;
	protected Collider2D triggerCollider;

	private void Awake()
	{
		listener = GetComponent<GameEventListener>();
		triggerCollider = GetComponent<Collider2D>();
	}

	private void OnEnable()
	{
		listener.OnGameEventTriggered += Activate;
	}

	private void Start()
	{
		triggerCollider.enabled = false;
	}

	private void OnDisable()
	{
		listener.OnGameEventTriggered -= Activate;
	}

	private void Activate()
	{
		triggerCollider.enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(gameEvent != null)
		{
			gameEvent.Invoke();
		}

		if(oneShot)
		{
			triggerCollider.enabled = false;
		}
	}
}
