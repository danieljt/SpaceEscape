using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A laser that patrols between points
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PatrolingLaser : MonoBehaviour
{
	[SerializeField] protected List<Transform> patrolPoints;
	[SerializeField] protected float velocity;

	protected const float minimumDistanceBeforeClamp = 0.1f;

	protected Rigidbody2D rbody;
	protected Transform lastTargetTransform;
	protected Transform nextTargetTransform;
	protected WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
	protected int patrolPointIndex;

	protected AudioSource audioSource;
	protected Animator animator;
	protected int onHash = Animator.StringToHash("isActive");

	private void Awake()
	{
		rbody = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
		animator = GetComponentInChildren<Animator>();
		SetStartPoint();	
	}

	private void OnEnable()
	{
		animator.SetBool(onHash, true);
	}

	private void Start()
	{
		if (patrolPoints != null)
		{
			StartCoroutine(Move());
			audioSource.Play();
		}
	}

	private void OnDisable()
	{
		animator.SetBool(onHash, false);
	}

	/// <summary>
	/// Move the laser between the points in the patrol points list using
	/// acceleration
	/// </summary>
	/// <returns></returns>
	protected IEnumerator Move()
	{
		nextTargetTransform = ChooseNextPatrolPoint();
		while (true)
		{
			bool hasLastTarget = lastTargetTransform != null;
			bool hasNextTarget = nextTargetTransform != null;

			if (hasLastTarget && hasNextTarget)
			{
				float lengthToNext = Vector2.SqrMagnitude(nextTargetTransform.transform.position - transform.position);

				while (lengthToNext > Mathf.Pow(minimumDistanceBeforeClamp,2))
				{
					Vector2 direction = (nextTargetTransform.transform.position - transform.position).normalized;
					rbody.MovePosition(rbody.position + velocity * Time.fixedDeltaTime * direction);
					lengthToNext = Vector2.SqrMagnitude(nextTargetTransform.transform.position - transform.position);
					yield return waitForFixedUpdate;
				}

				rbody.MovePosition(nextTargetTransform.position);
				lastTargetTransform = nextTargetTransform;
				nextTargetTransform = ChooseNextPatrolPoint();

				yield return waitForFixedUpdate;
			}

			else
			{
				yield break;
			}
		}
	}

	protected Transform ChooseNextPatrolPoint()
	{
		if(patrolPointIndex + 1 < patrolPoints.Count)
		{
			patrolPointIndex++;
		}

		else
		{
			patrolPointIndex = 0;
		}

		return patrolPoints[patrolPointIndex];
	}

	protected void SetStartPoint()
	{
		if(patrolPoints != null)
		{
			 for(int i=0; i<patrolPoints.Count; i++)
			{
				if(patrolPoints[i] != null)
				{
					lastTargetTransform = patrolPoints[i].transform;
					transform.position = lastTargetTransform.position;
					patrolPointIndex = i;
				}
			}
		}
	}
}
            