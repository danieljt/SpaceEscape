using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Display a text by increasing and then decreasing it's alpha
/// </summary>
[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Animator))]
public class DisplayText : MonoBehaviour
{
	protected Text text;
	protected Animator animator;
	protected AnimatorClipInfo clipInfo;
	protected int triggerHash = Animator.StringToHash("isActive");
	protected Color color;

	private void Awake()
	{
		text = GetComponent<Text>();
		animator = GetComponent<Animator>();
		color = text.color;
		color.a = 0;
		text.color = color;
	}

	/// <summary>
	/// Display each text in 
	/// </summary>
	/// <param name="duration"></param>
	/// <returns></returns>
	public IEnumerator Display()
	{
		animator.SetBool(triggerHash, true);
		yield return null;
		clipInfo = animator.GetCurrentAnimatorClipInfo(0)[0];
		float duration = clipInfo.clip.length;
		yield return new WaitForSeconds(duration);
	}
}
