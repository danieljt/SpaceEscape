using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class GameObjectMonitor : MonoBehaviour
{
    [SerializeField] protected GameEventListener disableEventListener;
	[SerializeField] protected GameEventListener enableEventListener;
    [SerializeField] protected GameObjectRuntimeSet disableSet;
	[SerializeField] protected GameObjectRuntimeSet enableSet;

	private void OnEnable()
	{
		if ((disableEventListener != null) && (enableEventListener != null))
		{
			disableEventListener.OnGameEventTriggered += DisableObjects;
			enableEventListener.OnGameEventTriggered += EnableObjects;
		}
	}

	private void OnDisable()
	{
		if ((disableEventListener != null) && (enableEventListener != null))
		{
			disableEventListener.OnGameEventTriggered -= DisableObjects;
			enableEventListener.OnGameEventTriggered += EnableObjects;
		}
	}

	private void DisableObjects()
	{
		if(disableSet)
		{
			for(int i = disableSet.Objects.Count-1; i >= 0; i--)
			{
				disableSet.Objects[i].gameObject.SetActive(false);
			}
		}
	}

	private void EnableObjects()
	{
		if (enableSet)
		{
			for (int i = enableSet.Objects.Count-1; i >= 0; i--)
			{
				enableSet.Objects[i].gameObject.SetActive(true);
			}
		}
	}
}
