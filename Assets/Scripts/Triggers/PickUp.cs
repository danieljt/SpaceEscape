using UnityEngine;

/// <summary>
/// A pickup can be collected by the player
/// </summary>
public class PickUp : MonoBehaviour
{
	[Tooltip("The level manager this script communicates with")]
	[SerializeField] protected LevelManager levelManager;

	private void Start()
	{
		if (levelManager != null)
		{
			levelManager.numberOfPickUps++;
		}	
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{		
		levelManager.pickUpsCollected++;
		Destroy(gameObject);		
	}
}
