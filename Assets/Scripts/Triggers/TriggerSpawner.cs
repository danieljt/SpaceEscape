using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class TriggerSpawner : Spawner
{
	[SerializeField] protected Vector2 startVelocity;
    protected GameEventListener listener;
	protected MovingLaser laser;

	private void Awake()
	{
		listener = GetComponent<GameEventListener>();
	}

	private void OnEnable()
	{
		listener.OnGameEventTriggered += SpawnAndSetVelocity;
	}

	private void OnDisable()
	{
		listener.OnGameEventTriggered -= SpawnAndSetVelocity;
	}

	private void SpawnAndSetVelocity()
	{
		Spawn();
		if (currentSpawned != null)
		{
			laser = currentSpawned.GetComponent<MovingLaser>();
			if (laser)
			{
				laser.Velocity = startVelocity;
			}
		}
	}
}
