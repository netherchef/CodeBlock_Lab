// Menuman v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menuman : MonoBehaviour
{
	// Components

	public GameObject[] buttons;

	// Variables

	public int currentButtonIndex;

	private void Update ()
	{
		if (Input.GetAxisRaw ("Vertical") > 0)
		{
			if (currentButtonIndex > 0)
			{
				currentButtonIndex--;
			}
		}
		else if (Input.GetAxisRaw ("Vertical") < 0)
		{
			if (currentButtonIndex < buttons.Length - 1)
			{
				currentButtonIndex++;
			}
		}
	}
}
