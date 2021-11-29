using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add this gameobject to the runtime list
/// </summary>
public class AddToGameObjectRuntimeSet : MonoBehaviour
{
	[Tooltip("The set this gameobject belongs to")]
    [SerializeField] protected GameObjectRuntimeSet set;

	[Tooltip("When to add and remove this gameobject.")]
	[SerializeField] protected AddBehaviour behaviour;

	[Tooltip("Is the gameobject disabled on start? Only works if behaviour is AwakeAndDestroy")]
	[SerializeField] protected bool isDisabledOnStart;

	private void Awake()
	{
		if(set && behaviour == AddBehaviour.AwakeAndDestroy)
		{
			set.Add(gameObject);
		}
	}

	private void OnEnable()
	{
		if(set && behaviour == AddBehaviour.EnableAndDisable)
		{
			set.Add(gameObject);
		}
	}

	private void Start()
	{
		if (behaviour == AddBehaviour.AwakeAndDestroy && isDisabledOnStart)
		{
			Debug.Log(gameObject);
			gameObject.SetActive(false);
		}
	}

	private void OnDisable()
	{
		if(set && behaviour == AddBehaviour.EnableAndDisable)
		{
			set.Remove(gameObject);
		}
	}

	private void OnDestroy()
	{
		if(set && behaviour == AddBehaviour.AwakeAndDestroy)
		{
			set.Remove(gameObject);
		}
	}
}

public enum AddBehaviour
{
	AwakeAndDestroy,
	EnableAndDisable
}
