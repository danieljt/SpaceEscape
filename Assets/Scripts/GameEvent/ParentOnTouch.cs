using UnityEngine;

/// <summary>
/// Parent the colliding game object to this gameobject
/// </summary>
public class ParentOnTouch : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		collision.gameObject.transform.SetParent(this.transform);
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		collision.gameObject.transform.SetParent(null);
	}
}
