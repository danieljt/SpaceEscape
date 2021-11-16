using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns an object
/// </summary>
public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    protected GameObject currentSpawned;

	public void Spawn()
	{
		if (prefabToSpawn != null)
		{
			if (currentSpawned != null)
			{
				currentSpawned.SetActive(true);
				currentSpawned.transform.position = transform.position;
			}

			else
			{
				currentSpawned = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
			}
		}
	}
}
