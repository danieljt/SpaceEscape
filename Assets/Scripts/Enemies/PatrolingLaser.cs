using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A laser that patrols between points
/// </summary>
public class PatrolingLaser : MonoBehaviour
{
	[SerializeField] protected List<Transform> patrolPoints;
	[SerializeField] protected float velocity;
	[SerializeField] protected float accelerationTime;
	[SerializeField] protected float accelerationDistance;

	protected Transform currentTarget;
	protected int patrolPointIndex;

	private void Start()
	{
		if (patrolPoints != null)
		{
			StartCoroutine(Move());
		}
	}

	/// <summary>
	/// Move the laser between the points in the patrol points list using
	/// acceleration
	/// </summary>
	/// <returns></returns>
	protected IEnumerator Move()
	{
		yield return null;
	}

	protected Transform ChooseNextPatrolPoint()
	{
		if(patrolPointIndex + 1 > patrolPoints.Count)
		{
			patrolPointIndex = 0;
		}
		else
		{
			patrolPointIndex++;
		}

		currentTarget = patrolPoints[patrolPointIndex];
		return currentTarget;
	}

	protected void SetStartPoint()
	{
		if(patrolPoints != null)
		{
			 
		}
	}
}
            