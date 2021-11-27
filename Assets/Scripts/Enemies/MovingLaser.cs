using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLaser : MonoBehaviour
{
	protected Vector2 velocity;
    protected Rigidbody2D rbody;
    protected Animator animator;
	protected int onHash = Animator.StringToHash("isActive");

	private void Awake()
	{
		rbody = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
	}

	private void OnEnable()
	{
		animator.SetBool(onHash, true);
	}

	private void OnDisable()
	{
		animator.SetBool(onHash, false);
	}

	private void FixedUpdate()
	{
		rbody.MovePosition(rbody.position + velocity*Time.fixedDeltaTime);
	}

	public Vector2 Velocity
	{
		set { velocity = value; }
	}
}
