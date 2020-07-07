// Planter v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
	private void Update ()
	{
		if (Input.GetMouseButtonDown (0))
		{
			Plant ();
		}
	}

	private void Plant ()
	{
		print ("Click!");
	}
}
