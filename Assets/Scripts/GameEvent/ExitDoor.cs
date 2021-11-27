using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script controls a door with an entrance that can be opened
/// </summary>
[RequireComponent(typeof(GameEventListener))]
[RequireComponent(typeof(Collider2D))]
public class ExitDoor : MonoBehaviour
{
	[Tooltip("The game manager")]
	[SerializeField] protected GameManager manager;

	[Tooltip("The scene to load")]
	[SerializeField] protected int sceneToLoad;

	[Tooltip("The door to deactivate when the event is received")]
    [SerializeField] protected GameObject door;

	protected GameEventListener listener;
	protected Collider2D triggerCollider;

	private void Awake()
	{
		listener = GetComponent<GameEventListener>();
		triggerCollider = GetComponent<Collider2D>();
		triggerCollider.enabled = false;
	}

	private void OnEnable()
	{
		listener.OnGameEventTriggered += Toggle;
	}

	private void OnDisable()
	{
		listener.OnGameEventTriggered -= Toggle;
	}

	private void Toggle()
	{
		door.SetActive(false);
		triggerCollider.enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(manager != null)
		{
			manager.LoadNextLevel(sceneToLoad);
		}
	}
}
