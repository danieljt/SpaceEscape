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
	private void DisplayWinScreen(int scene, int pickUpsCollected, int numberOfPickUps)
	{
		if (winScreenSubMenu && levelCompleteText && pickUpsCollectedText && pickUpsTotalText)
		{
			WinGame();
			levelCompleteText.text = "Level " + scene + " Completed";
			pickUpsCollectedText.text = "Pick ups collected: " + pickUpsCollected;
			pickUpsTotalText.text = "Pick ups Total: " + numberOfPickUps;
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
