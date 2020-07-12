// Planter v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Structs ________________________________________________________________

[System.Serializable]
public struct PlanterCategory
{
	public string name;
	public KeyCode hotkey;
	public Type[] types;
}

[System.Serializable]
public struct Type
{
	public GameObject item;
}

#endregion

public class Planter : MonoBehaviour
{
	// Components

	public Camera cam;
	public Text modeDisplay;
	public Transform holder;

	private GameObject mouseHoverDisplay;

	// Variables

	public PlanterCategory[] categories;

	private int currentTypeIndex;

	private string currentCategory;

	private void Start ()
	{
		// On game start, activate an arbitrary mode

		if (!mouseHoverDisplay)
		{
			// Set the current mode

			currentCategory = categories[0].name;

			// Update the mode display

			modeDisplay.text = currentCategory;

			for (int c = 0; c < categories.Length; c++)
			{
				if (categories[c].name == currentCategory)
				{
					// Spawn clone

					mouseHoverDisplay = Instantiate (categories[c].types[0].item, MousePosition (), Quaternion.identity, transform);
				}
			}
		}
	}

	private void Update ()
	{
		// Plant

		if (Input.GetMouseButtonDown (0))
		{
			GameObject newInstance = Instantiate (mouseHoverDisplay, MousePosition (), Quaternion.identity, transform);

			try
			{
				Transform existingDirectory = GameObject.Find (currentCategory).transform;

				newInstance.transform.SetParent (existingDirectory);
			}
			catch
			{
				GameObject newDirectory = new GameObject (currentCategory);
				newDirectory.transform.SetParent (holder);

				newInstance.transform.SetParent (newDirectory.transform);
			}

			return;
		}

		// Mode Change

		if (Input.anyKeyDown)
		{
			for (int c = 0; c < categories.Length; c++)
			{
				if (Input.GetKeyDown (categories[c].hotkey))
				{
					// Change the current mode

					currentCategory = categories[c].name;

					// Reset clone type index

					currentTypeIndex = 0;

					// Update the mode display

					modeDisplay.text = currentCategory;

					// Replace clone

					Replace_MouseDisplay (categories[c].types[0].item);
				}
			}

			return;
		}

		// Type

		if (Scroll_Up ())
		{
			for (int c = 0; c < categories.Length; c++)
			{
				if (categories[c].name == currentCategory)
				{
					if (categories[c].types.Length > currentTypeIndex + 1)
					{
						currentTypeIndex++;

						Replace_MouseDisplay (categories[c].types[currentTypeIndex].item);
					}
				}
			}

			return;
		}
		else if (Scroll_Down ())
		{
			for (int c = 0; c < categories.Length; c++)
			{
				if (categories[c].name == currentCategory)
				{
					if (currentTypeIndex > 0)
					{
						currentTypeIndex--;

						Replace_MouseDisplay (categories[c].types[currentTypeIndex].item);
					}
				}
			}

			return;
		}

		// Position clone at mouse

		if (mouseHoverDisplay) mouseHoverDisplay.transform.position = MousePosition ();
	}

	private void Replace_MouseDisplay (GameObject clone)
	{
		// Destroy current clone

		Destroy (mouseHoverDisplay);

		// Spawn clone

		mouseHoverDisplay = Instantiate (clone, MousePosition (), Quaternion.identity, transform);
	}

	#region Mouse ______________________________________________________________

	private Vector2 MousePosition ()
	{
		Vector3 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);
		return new Vector2 (mousePos.x, mousePos.y);
	}

	private bool Scroll_Up ()
	{
		return Input.mouseScrollDelta.y < 0;
	}

	private bool Scroll_Down ()
	{
		return Input.mouseScrollDelta.y > 0;
	}

	#endregion
}
