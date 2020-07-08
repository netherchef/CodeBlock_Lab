// Planter v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Clone
{
	public string modeName;
	public GameObject prefab;
	public KeyCode hotkey;
}

public class Planter : MonoBehaviour
{
	// Components

	public Camera cam;

	// Variables

	public Clone[] clones;

	public string currentMode = "Box";

	private void Update ()
	{
		// Plant

		if (Input.GetMouseButtonDown (0))
		{
			for (int i = 0; i < clones.Length; i++)
			{
				if (clones[i].modeName == currentMode)
				{
					Plant (clones[i].prefab, Plant_Position ());
				}
			}

			return;
		}

		// Mode Change

		if (Input.anyKeyDown)
		{
			for (int c = 0; c < clones.Length; c++)
			{
				if (Input.GetKeyDown (clones[c].hotkey))
				{
					currentMode = clones[c].modeName;
				}
			}
		}
	}

	private void Plant (GameObject prefab, Vector3 pos)
	{
		Instantiate (prefab, pos, Quaternion.identity, transform);
	}

	private Vector2 Plant_Position ()
	{
		Vector3 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);
		return new Vector2 (mousePos.x, mousePos.y);
	}
}
