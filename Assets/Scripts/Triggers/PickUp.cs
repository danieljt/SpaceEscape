using UnityEngine;

/// <summary>
/// A pickup can be collected by the player
/// </summary>
public class PickUp : MonoBehaviour
{
	[Tooltip("The level manager this script communicates with")]
	[SerializeField] protected LevelManager levelManager;

	protected AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();	
	}

	private void Start()
	{
		if (levelManager != null)
		{
			levelManager.numberOfPickUps++;
		}	
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
		levelManager.pickUpsCollected++;
		Destroy(gameObject);		
	}
}
