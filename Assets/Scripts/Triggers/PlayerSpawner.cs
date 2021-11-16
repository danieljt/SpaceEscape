using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Spawns the player
/// </summary>
public class PlayerSpawner : MonoBehaviour
{
	public GameObject player;
	public CinemachineVirtualCamera vcam;
	private GameObject currentPlayer;

	private void Awake()
	{
		Spawn();
	}

	private void OnEnable()
	{
		if(currentPlayer != null)
		{
			Death death = currentPlayer.GetComponent<Death>();
			if(death)
			{
				death.OnDeath += Spawn; 
			}
		}
	}

	private void OnDisable()
	{
		if (currentPlayer != null)
		{
			Death death = currentPlayer.GetComponent<Death>();
			if (death)
			{
				death.OnDeath -= Spawn;
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
	}
}
