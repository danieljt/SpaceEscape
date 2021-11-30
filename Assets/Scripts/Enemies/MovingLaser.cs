using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLaser : MonoBehaviour
{
	protected Vector2 velocity;
    protected Rigidbody2D rbody;
    protected Animator animator;
	protected AudioSource audioSource;
	protected int onHash = Animator.StringToHash("isActive");

	private void Awake()
	{
		rbody = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		animator.SetBool(onHash, true);
		audioSource.Play();
	}

	private void OnDisable()
	{
		animator.SetBool(onHash, false);
		audioSource.Stop();
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
