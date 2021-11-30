using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the flow of a menu in the unity UI system. A menuController communicates
/// directly with the game manager.
/// </summary>
public class MenuController : MonoBehaviour
{
    [SerializeField] protected GameManager gameManager;
	[SerializeField] protected GameObject firstActivated;

	protected SubMenuController[] subMenus;

	private void Awake()
	{
		//Cursor.visible = false;
		subMenus = GetComponentsInChildren<SubMenuController>();
	}

	private void Start()
	{
		SetUpSubMenus();
	}

	private void SetUpSubMenus()
	{
		if (subMenus != null)
		{
			for (int i = 0; i < subMenus.Length; i++)
			{
				if (subMenus[i] != null)
				{
					if (subMenus[i].gameObject != firstActivated)
					{
						subMenus[i].DisableCanvas();
					}

					else
					{
						subMenus[i].EnableCanvas();
					}
				}
			}
		}
	}

	public void StartNewGame()
	{
		if(gameManager != null)
		{
			gameManager.LoadStartLevel();
		}
	}

	public void ExitGame()
	{
		if(gameManager != null)
		{
			gameManager.QuitGame();
		}
	}

	public void MainMenu()
	{
		if(gameManager != null)
		{
			gameManager.LoadMainMenu();
		}
	}

	public void Display(SubMenuController subMenu)
	{
		if(subMenu == null)
		{
			return;
		}

		for(int i = 0; i < subMenus.Length; i++)
		{
			if(subMenus[i] != subMenu)
			{
				subMenus[i].DisableCanvas();
			}

			else
			{
				subMenus[i].EnableCanvas();
			}
		}
	}

	public void Hide()
	{
		for (int i = 0; i < subMenus.Length; i++)
		{
			if (subMenus[i] != null)
			{
				subMenus[i].DisableCanvas();
			}
		}
	}
}
