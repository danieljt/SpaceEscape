using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryMenuController : MenuController
{
	[SerializeField] protected Text pickUpsText;
	[SerializeField] protected Text pickUpsTotalText;
	[SerializeField] protected Text deathsText;

	private void Awake()
	{
		if(pickUpsText && pickUpsTotalText && deathsText && gameManager)
		{
			pickUpsText.text = "Pick ups collected: " + gameManager.PickUpsCollected;
			pickUpsTotalText.text = "Total pickups in game: " + gameManager.PickUpsTotal;
			deathsText.text = "Total number of deaths: " + gameManager.TotalDeaths;
		}
	}
}
