using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
	protected AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponentInChildren<AudioSource>();
	}

	private void Start()
	{
		audioSource.Play();
	}
}
