using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for components that add forces or velocity to the player.
/// </summary>
public interface IExternalForce
{
	public Vector2 AddForce();
	public Vector2 AddVelocity();
	public Vector2 AddImpulse();
}
