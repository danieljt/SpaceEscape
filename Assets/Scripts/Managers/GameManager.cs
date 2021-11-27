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

	public event Action OnPause;
	public event Action OnResume;

	// The next level to load
	protected int nextLevel;

	// The total number of pickups collected and found during the application
	protected int pickUpsCollected;
	protected int pickUpsTotal;

	private void Awake()
	{
		Reset();
	}

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
		Time.timeScale = 0;
		nextLevel = nextScene;
		pickUpsCollected += newPickUpsCollected;
		pickUpsTotal += newPickUpsTotal;
	}

	/// <summary>
	/// Load the level with the given buils index
	/// </summary>
	/// <param name="scene"></param>
	public void LoadLevel(int scene)
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
		Time.timeScale = 1;
		OnSceneLoaded?.Invoke();
	}

	/// <summary>
	/// Loads the first playable level. This is just a utility function
	/// </summary>
	public void LoadStartLevel()
	{
		Reset();
		LoadLevel(1);
	}

	/// <summary>
	/// Load main menu
	/// </summary>
	public void LoadMainMenu()
	{
		Reset();
		LoadLevel(0);
	}

	/// <summary>
	/// Quit the game
	/// </summary>
	public void QuitGame()
	{
		Reset();
		Application.Quit();
	}

	/// <summary>
	/// Reset the game manager. This should be used each time a new game starts because data
	/// in a scriptable object is kept during playtime.
	/// </summary>
	private void Reset()
	{
		pickUpsCollected = 0;
		pickUpsTotal = 0;
	}

	public void Pause()
	{
		Time.timeScale = 0;
		OnPause?.Invoke();
	}

	public void Resume()
	{
		Time.timeScale = 1;
		OnResume?.Invoke();
	}
}
