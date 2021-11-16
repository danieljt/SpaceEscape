using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ConstantVelocity : MonoBehaviour
{
    public Vector2 velocity;
	protected Rigidbody2D rbody;

	private void Awake()
	{
		rbody = GetComponent<Rigidbody2D>();	
	}

	private void FixedUpdate()
	{
		rbody.velocity = velocity;
	}
}
