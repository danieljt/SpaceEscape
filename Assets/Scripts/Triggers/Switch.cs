using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component for a switch
/// </summary>
[RequireComponent(typeof(TriggerOnTouch))]
public class Switch : MonoBehaviour
{
    protected Animator animator;
	protected TriggerOnTouch triggerComponent;
	protected int triggerHash = Animator.StringToHash("isActive");
	protected bool isActivated;

	private void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		triggerComponent = GetComponent<TriggerOnTouch>();
	}

	private void OnEnable()
	{
		triggerComponent.OnTriggered += Activate;
	}

	private void OnDisable()
	{
		triggerComponent.OnTriggered -= Activate;
	}

	private void Activate()
	{
		if(!isActivated)
		{
			animator.SetTrigger(triggerHash);
			isActivated = true;
		}
	}
}
