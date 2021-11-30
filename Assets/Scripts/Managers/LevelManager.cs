using System;
using UnityEngine;

/// <summary>
/// A level manager keeps information about the current level. This is a singleton class
/// that is destroyed at the end of the scene.
/// </summary>
[CreateAssetMenu(menuName = "Management/LevelManager")]
public class LevelManager : ScriptableObject
{
	[Tooltip("The game manager this level manager communicates with")]
	public GameManager gameManager;

	// This is the number of pickups in this level. This number is set in start.
	[HideInInspector] public int numberOfPickUps;

	// This is the number of pick ups the player has collected this scene.
	[HideInInspector] public int pickUpsCollected;

	// This is the number of deaths in the current level
	[HideInInspector] public int totalDeaths;

	// These events are fired when the level has been completed
	public event Action<int, int, int, int> OnLevelComplete;

	private void OnEnable()
	{
		if (gameManager != null)
		{
			gameManager.OnSceneLoaded += Reset;
			OnLevelComplete += gameManager.LevelComplete;
		}
	}

	private void OnDisable()
	{
		if (gameManager != null)
		{
			gameManager.OnSceneLoaded -= Reset;
			OnLevelComplete += gameManager.LevelComplete;
		}
	}

	public void Reset()
	{
		pickUpsCollected = 0;
		numberOfPickUps = 0;
		totalDeaths = 0;
	}

	public void LevelComplete(int scene)
	{
		OnLevelComplete?.Invoke(scene, pickUpsCollected, numberOfPickUps, totalDeaths);
	}
}
