using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the game flow of the application. Any object that needs to communicate with
/// the game manager can add it as a public variable
/// </summary>
[CreateAssetMenu(menuName = "Management/GameManager")]
public class GameManager : ScriptableObject
{
	// Event is called when a new scene is loaded
	public event Action OnSceneLoaded;

	// The next level to load
	protected int nextLevel;

	// The total number of pickups collected and found during the application
	protected int pickUpsCollected;
	protected int pickUpsTotal;

	// The pickups found in the current scene
	protected int pickUpsCollectedCurrentScene;
	protected int pickUpsTotalThisCurrentScene;

	private void OnEnable()
	{
		SceneManager.sceneLoaded += NewLevelLoaded;
	}

	protected void OnDisable()
	{
		SceneManager.sceneLoaded -= NewLevelLoaded;
	}

	/// <summary>
	/// The level is complete
	/// </summary>
	/// <param name="nextScene"></param>
	public void LevelComplete(int nextScene, int newPickUpsCollected, int newPickUpsTotal)
	{
		nextLevel = nextScene;
		pickUpsCollectedCurrentScene = newPickUpsCollected;
		pickUpsTotalThisCurrentScene = newPickUpsTotal;
		pickUpsCollected += newPickUpsCollected;
		pickUpsTotal += newPickUpsTotal;
		LoadNextLevel(nextLevel);
	}

	/// <summary>
	/// Load the next level
	/// </summary>
	/// <param name="scene"></param>
	public void LoadNextLevel(int scene)
	{
		if(SceneManager.GetSceneByBuildIndex(scene) != null)
		{
			AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(scene);
			sceneLoadOperation.allowSceneActivation = true;
		}
		else
		{
			Debug.LogWarning("Scene does not exist. Check your build settings");
		}
	}

	/// <summary>
	/// Event is run when a new scene is loaded
	/// </summary>
	public void NewLevelLoaded(Scene newScene, LoadSceneMode mode)
	{
		OnSceneLoaded?.Invoke();
	}
}
