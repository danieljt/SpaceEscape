using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Spawns the player
/// </summary>
public class PlayerSpawner : MonoBehaviour
{
	public LevelManager levelManager;
	public GameObject player;
	public CinemachineVirtualCamera vcam;
	private GameObject currentPlayer;
	protected AudioSource spawnAudio;
	protected Death death;

	private void Awake()
	{
		spawnAudio = GetComponent<AudioSource>();
		Spawn();
	}

	private void OnEnable()
	{
		if(currentPlayer != null)
		{
			death = currentPlayer.GetComponent<Death>();
			if(death)
			{
				death.OnDeath += SpawnPlayer; 
			}
		}
	}

	private void OnDisable()
	{
		if (currentPlayer != null)
		{
			death = currentPlayer.GetComponent<Death>();
			if (death)
			{
				death.OnDeath -= SpawnPlayer;
			}
		}
	}

	private void Spawn()
	{
		if(player != null)
		{
			if(currentPlayer != null)
			{
				currentPlayer.SetActive(true);
				currentPlayer.transform.position = transform.position;
			}

			else
			{
				currentPlayer = Instantiate(player, transform.position, Quaternion.identity);
			}

			if (vcam != null)
			{
				vcam.Follow = currentPlayer.transform;
			}
		}

		spawnAudio.PlayOneShot(spawnAudio.clip);
	}

	private void SpawnPlayer()
	{
		levelManager.totalDeaths++;
		Spawn();
	}
}
