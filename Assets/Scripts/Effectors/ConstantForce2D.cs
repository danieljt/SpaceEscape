using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantForce2D : MonoBehaviour, IExternalForce
{
	[Tooltip("Force added from this component")]
	public Vector2 force;
	[Tooltip("Velocity added from this component")]
	public Vector2 velocity;

	/// <summary>
	/// returns the force vector 
	/// </summary>
	/// <returns></returns>
	public Vector2 AddForce()
	{
		return force;
	}

	/// <summary>
	/// returns the velocity vector
	/// </summary>
	/// <returns></returns>
	public Vector2 AddImpulse()
	{
		return force;
	}

	public Vector2 AddVelocity()
	{
		return velocity;
	}
}
