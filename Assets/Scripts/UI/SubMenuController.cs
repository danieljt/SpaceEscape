using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A submenu controller
/// </summary>
[RequireComponent(typeof(Canvas))]
public class SubMenuController : MonoBehaviour
{
	[Tooltip("This object is selected when the canvas is enabled")]
	[SerializeField] protected GameObject firstSelected;
	protected Canvas canvas;

	private void Awake()
	{
		canvas = GetComponent<Canvas>();
	}

	/// <summary>
	/// Toggle the canvas. If the canvas is enabled, set its current active gameobject
	/// to this scripts first selected.
	/// </summary>
	public void Toggle()
	{
		canvas.enabled = !canvas.enabled;

		if(canvas.enabled)
		{
			SetEventSystemFirstSelected();
		}
	}

	/// <summary>
	/// Disable the canvas
	/// </summary>
	public void DisableCanvas()
	{
		canvas.enabled = false;
	}

	/// <summary>
	/// Enable canvas
	/// </summary>
	public void EnableCanvas()
	{
		canvas.enabled = true;
		SetEventSystemFirstSelected();
	}

	/// <summary>
	/// Set the event system first selected to this components first selected
	/// </summary>
	private void SetEventSystemFirstSelected()
	{
		EventSystem.current.SetSelectedGameObject(firstSelected);
	}
}
