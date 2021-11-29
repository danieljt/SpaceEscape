using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// An exit from a scene
/// </summary>
public class SceneExit : MonoBehaviour
{
	[Tooltip("The level manager this script communicates with")]
	public LevelManager levelManager;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(levelManager != null)
		{
			levelManager.LevelComplete(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
