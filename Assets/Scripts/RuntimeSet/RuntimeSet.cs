using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A runtime Set keeps a list of objects during runtime.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class RuntimeSet<T> : ScriptableObject
{
    [SerializeField] protected List<T> objects = new List<T>();

    public void Add(T newObject)
    {
        if (!objects.Contains(newObject))
            objects.Add(newObject);
    }

    public void Remove(T oldObject)
    {
        if (objects.Contains(oldObject))
            objects.Remove(oldObject);
    }

    public List<T> Objects
	{
		get { return objects; }
	}
}
