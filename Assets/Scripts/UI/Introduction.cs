using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the introduction text before a new game
/// </summary>
public class Introduction : MonoBehaviour
{
	[SerializeField] protected List<DisplayText> texts;
	[SerializeField] protected bool runOnStart;

	public void Display()
	{
		StartCoroutine(DisplayText());
	}

	private void Start()
	{
		if(runOnStart)
		{
			StartCoroutine(DisplayText());
		}
	}

	/// <summary>
	/// Display the texts in order
	/// </summary>
	public IEnumerator DisplayText()
	{
		if (texts != null)
		{
			for (int i = 0; i < texts.Count; i++)
			{
				if (texts[i] != null)
				{
					yield return StartCoroutine(texts[i].Display());
				}
			}
		}
	}
}
