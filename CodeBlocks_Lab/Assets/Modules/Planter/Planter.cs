// Planter v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
	// Components

	public Camera cam;

	// Variables

	public GameObject clone;

	private void Update ()
	{
		if (Input.GetMouseButtonDown (0))
		{
			Plant ();
		}
	}

	private void Plant ()
	{
		Instantiate (clone, Plant_Position (), Quaternion.identity, transform);
	}

	private Vector2 Plant_Position ()
	{
		Vector3 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);
		return new Vector2 (mousePos.x, mousePos.y);
	}
}
