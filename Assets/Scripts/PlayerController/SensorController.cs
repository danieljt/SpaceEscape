using System;
using UnityEngine;

public class SensorController : MonoBehaviour
{
	[Tooltip("What we define as ground")]
	public Vector2 boxSize;
	public LayerMask whatIsValid;

	// This action is fired when the box collider comes into contact with ground and when
	// it leaves the ground.
	public Action<bool> isTouching;

	private void FixedUpdate()
	{
		bool hit = Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, 0.05f, whatIsValid);
		isTouching?.Invoke(hit);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawCube(gameObject.transform.position, boxSize);
	}
}
