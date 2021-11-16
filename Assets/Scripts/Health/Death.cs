using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add this to a gameobject to allow it to die. Any object that can die
/// should have a health component
/// </summary>
[RequireComponent(typeof(Health))]
public class Death : MonoBehaviour
{
	[Tooltip("How does the object die?")]
	[SerializeField] protected DeathBehaviour deathBehaviour;
	[Tooltip("How many seconds it should take before death happens")]
	[SerializeField] protected float secondsBeforeDeath;
    protected Health health;

	// Called when this object dies
	public event Action OnDeath;

	private void Awake()
	{
		health = GetComponent<Health>();
	}

	private void OnEnable()
	{
		health.OnHealthZero += Die;
	}

	private void OnDisable()
	{
		health.OnHealthZero -= Die;
	}

	protected void Die()
	{
		StartCoroutine(DieInSeconds(secondsBeforeDeath));
	}

	protected IEnumerator DieInSeconds(float time)
	{
		yield return new WaitForSeconds(time);
		switch (deathBehaviour)
		{
			case (DeathBehaviour.DestroyObject):
				Destroy(gameObject);
				break;

			case DeathBehaviour.DisableObject:
				gameObject.SetActive(false);
				break;
		}

		OnDeath?.Invoke();
		yield return null;
	}
}

/// <summary>
/// Enum telling how an object should die. There are two ways of dying. Either the
/// object is destroyed or it is disabled. Disabling objects is recommended if many 
/// gameobjects are spawned and destroyed due to garbage collector.
/// </summary>
public enum DeathBehaviour
{
	DisableObject,
	DestroyObject
}
