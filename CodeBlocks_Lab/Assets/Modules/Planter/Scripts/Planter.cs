﻿// Planter v0.02

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

	[Header ("Components:")]

	public Camera cam;
	public Text modeDisplay;
	[Tooltip (" All objects loaded from the JSON File go here! ")]
	public Transform holder;

	private GameObject mouseHoverDisplay;

	// Variables

	[Header ("Variables:")]

	public bool save;
	public bool load;

	public PlanterCategory[] categories;
	public bool clear;

	private int currentTypeIndex;
	private string currentCategory;

	[Header ("Autosave:")]

	public bool disableAutosave;

	private float autosaveTimer;

	private void Start ()
	{
		// On game start, activate an arbitrary category.
		// If that fails, find the problem and log a warning.

		try
		{
			if (!mouseHoverDisplay) Activate_Category (categories[0].name);
		}
		catch
		{
			if (categories.Length == 0) Debug.LogWarning ("No categories set yo.");
			if (!holder) Debug.LogWarning ("No holder set yo.");
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

		// Category Change

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

		// Types

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

		// Position spawned object at mouse

		if (mouseHoverDisplay) mouseHoverDisplay.transform.position = MousePosition ();

		// Autosave

		if (!disableAutosave)
		{
			if (autosaveTimer < 10f)
			{
				autosaveTimer += Time.deltaTime;
			}
			else
			{
				save = true;

				autosaveTimer = 0f;
			}
		}
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

	private void Activate_Category (string targCategory)
	{
		// Set the current Category

		currentCategory = targCategory;

		// Update the mode display

		modeDisplay.text = currentCategory;

		for (int c = 0; c < categories.Length; c++)
		{
			if (categories[c].name == currentCategory)
			{
				// Spawn new object on the mouse

				mouseHoverDisplay = Instantiate (categories[c].types[0].item, MousePosition (), Quaternion.identity, transform);
			}
		}
	}
}