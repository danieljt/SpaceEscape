using UnityEngine;

/// <summary>
/// Trigger a game event when touched
/// </summary>
public class TriggerOnTouch : MonoBehaviour
{
	[Tooltip("The event to invoke on touch")]
    [SerializeField] protected GameEvent gameEvent;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(gameEvent != null)
		{
			gameEvent.Invoke();
		}
	}
}
