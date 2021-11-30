using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameMenuController : MenuController
{
    [SerializeField] protected LevelManager levelManager;
	[SerializeField] protected SubMenuController pauseScreenMenu;
	[SerializeField] protected SubMenuController winScreenSubMenu;
	[SerializeField] protected Text levelCompleteText;
	[SerializeField] protected Text pickUpsCollectedText;
	[SerializeField] protected Text pickUpsTotalText;
	[SerializeField] protected Text deathsTotalText;

	private void OnEnable()
	{
		if(levelManager != null)
		{
			levelManager.OnLevelComplete += DisplayWinScreen;
		}

		if(gameManager != null)
		{
			gameManager.OnPause += PauseGame;
		}
	}

	private void OnDisable()
	{
		if (levelManager != null)
		{
			levelManager.OnLevelComplete -= DisplayWinScreen;
		}

		if(gameManager != null)
		{
			gameManager.OnPause -= PauseGame;
		}
	}

	/// <summary>
	/// Display the win screen for the level
	/// </summary>
	private void DisplayWinScreen(int scene, int pickUpsCollected, int numberOfPickUps, int totalDeaths)
	{
		if (winScreenSubMenu && levelCompleteText && pickUpsCollectedText && pickUpsTotalText && deathsTotalText)
		{
			WinGame();
			levelCompleteText.text = "Level " + scene + " Completed";
			pickUpsCollectedText.text = "Pick ups collected: " + pickUpsCollected;
			pickUpsTotalText.text = "Pick ups Total: " + numberOfPickUps;
			deathsTotalText.text = "Deaths in level: " + totalDeaths;
		}

		else
		{
			Debug.LogError(this + " Some values not set in the inspector");
		}
	}

	public void WinGame()
	{
		Display(winScreenSubMenu);
	}

	public void PauseGame()
	{
		Display(pauseScreenMenu);
	}

	public void ResumeGame()
	{
		Hide();
		gameManager.Resume();
	}

	public void NextLevel(int level)
	{
		gameManager.LoadLevel(level);
	}
}
