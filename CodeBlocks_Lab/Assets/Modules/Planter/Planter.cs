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

	private GameObject currentClone;

	// Variables

	public Clone[] clones;

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

					// Update the mode display

					modeDisplay.text = currentMode;

					// Destroy current clone

					Destroy (currentClone);

					// Spawn clone

					GameObject clone = Instantiate (clones[c].prefabs[0], MousePosition (), Quaternion.identity, transform);

					currentClone = clone;
				}
			}

			return;
		}

		// Position clone at mouse

		if (currentClone) currentClone.transform.position = MousePosition ();
	}

	private Vector2 MousePosition ()
	{
		Vector3 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);
		return new Vector2 (mousePos.x, mousePos.y);
	}
}
