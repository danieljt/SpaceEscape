using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : SceneExit
{
	[SerializeField] protected bool activeOnStart;
    protected Animator animator;
	protected int isActiveHash = Animator.StringToHash("isActive");

	private void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		if(activeOnStart)
		{
			animator.SetTrigger(isActiveHash);
		}
	}
}
