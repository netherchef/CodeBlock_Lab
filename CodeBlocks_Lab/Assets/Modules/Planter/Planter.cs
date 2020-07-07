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
		Vector3 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);
		Vector2 mousePos2d = new Vector2 (mousePos.x, mousePos.y);

		Instantiate (clone, mousePos2d, Quaternion.identity);
	}
}
