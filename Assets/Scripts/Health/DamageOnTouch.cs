using UnityEngine;

/// <summary>
/// Add this component to a gameobject to allow it to hurt other gameobjects
/// The gameobject must have a health component
/// </summary>
public class DamageOnTouch : MonoBehaviour
{
	[Tooltip("The damage done")]
	public int damage;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Health health = collision.gameObject.GetComponent<Health>();
		if(health)
		{
			health.LoseHealth(damage);
		}
	}
}
