// Planter v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Clone
{
	public string modeName;
	public GameObject[] prefabs;
	public KeyCode hotkey;
}

public class Planter : MonoBehaviour
{
	// Components

	public Camera cam;
	public Text modeDisplay;

	public GameObject currentClone;

	// Variables

	public Clone[] clones;

	private int prefabIndex;

	private string currentMode = "Box";

	private void Start ()
	{
		if (!currentClone)
		{
			for (int c = 0; c < clones.Length; c++)
			{
				if (clones[c].modeName == currentMode)
				{
					// Spawn clone

					GameObject clone = Instantiate (clones[c].prefabs[0], MousePosition (), Quaternion.identity, transform);

					currentClone = clone;
				}
			}
		}
	}

	private void Update ()
	{
		// Plant

		if (Input.GetMouseButtonDown (0))
		{
			Instantiate (currentClone, MousePosition (), Quaternion.identity, transform);

			return;
		}

		// Mode Change

		if (Input.anyKeyDown)
		{
			for (int c = 0; c < clones.Length; c++)
			{
				if (Input.GetKeyDown (clones[c].hotkey))
				{
					// Change the current mode

					currentMode = clones[c].modeName;

					// Reset clone type index

					prefabIndex = 0;

					// Update the mode display

					modeDisplay.text = currentMode;

					// Replace clone

					Replace_Clone (clones[c].prefabs[0]);
				}
			}

			return;
		}

		// Type

		if (Scroll_Up ())
		{
			for (int c = 0; c < clones.Length; c++)
			{
				if (clones[c].modeName == currentMode)
				{
					if (clones[c].prefabs.Length > prefabIndex + 1)
					{
						prefabIndex++;

						Replace_Clone (clones[c].prefabs[prefabIndex]);
					}
				}
			}

			return;
		}
		else if (Scroll_Down ())
		{
			for (int c = 0; c < clones.Length; c++)
			{
				if (clones[c].modeName == currentMode)
				{
					if (prefabIndex > 0)
					{
						prefabIndex--;

						Replace_Clone (clones[c].prefabs[prefabIndex]);
					}
				}
			}

			return;
		}

		// Position clone at mouse

		if (currentClone) currentClone.transform.position = MousePosition ();
	}

	private void Replace_Clone (GameObject clone)
	{
		// Destroy current clone

		Destroy (currentClone);

		// Spawn clone

		currentClone = Instantiate (clone, MousePosition (), Quaternion.identity, transform);
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
