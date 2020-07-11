// Planter v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Mode
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

public class Planter : MonoBehaviour
{
	// Components

	public Camera cam;
	public Text modeDisplay;
	public GameObject container;

	private GameObject mouseHoverDisplay;

	// Variables

	public Mode[] modes;

	private int currentTypeIndex;

	private string currentMode;

	private void Start ()
	{
		// On game start, activate an arbitrary mode

		if (!mouseHoverDisplay)
		{
			// Set the current mode

			currentMode = modes[0].name;

			// Update the mode display

			modeDisplay.text = currentMode;

			for (int c = 0; c < modes.Length; c++)
			{
				if (modes[c].name == currentMode)
				{
					// Spawn clone

					mouseHoverDisplay = Instantiate (modes[c].types[0].item, MousePosition (), Quaternion.identity, transform);
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
				Transform existingDirectory = GameObject.Find (currentMode).transform;

				newInstance.transform.SetParent (existingDirectory);
			}
			catch
			{
				GameObject newDirectory = new GameObject (currentMode);
				newDirectory.transform.SetParent (container.transform);

				newInstance.transform.SetParent (newDirectory.transform);
			}

			return;
		}

		// Mode Change

		if (Input.anyKeyDown)
		{
			for (int c = 0; c < modes.Length; c++)
			{
				if (Input.GetKeyDown (modes[c].hotkey))
				{
					// Change the current mode

					currentMode = modes[c].name;

					// Reset clone type index

					currentTypeIndex = 0;

					// Update the mode display

					modeDisplay.text = currentMode;

					// Replace clone

					Replace_MouseDisplay (modes[c].types[0].item);
				}
			}

			return;
		}

		// Type

		if (Scroll_Up ())
		{
			for (int c = 0; c < modes.Length; c++)
			{
				if (modes[c].name == currentMode)
				{
					if (modes[c].types.Length > currentTypeIndex + 1)
					{
						currentTypeIndex++;

						Replace_MouseDisplay (modes[c].types[currentTypeIndex].item);
					}
				}
			}

			return;
		}
		else if (Scroll_Down ())
		{
			for (int c = 0; c < modes.Length; c++)
			{
				if (modes[c].name == currentMode)
				{
					if (currentTypeIndex > 0)
					{
						currentTypeIndex--;

						Replace_MouseDisplay (modes[c].types[currentTypeIndex].item);
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
