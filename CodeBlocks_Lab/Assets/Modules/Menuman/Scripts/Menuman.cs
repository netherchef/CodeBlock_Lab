// Menuman v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menuman : MonoBehaviour
{
	// Components

	public GameObject[] buttons;

	public MenuFunctions menuFunctions;

	// Variables

	private bool directionalReceived;

	public int currentButtonIndex;

	public string selectButton = "Submit";

	private void Update ()
	{
		if (Input.GetButtonDown (selectButton))
		{
			Perform_Menu_Function ();

			return;
		}

		if (Input.GetAxisRaw ("Vertical") != 0)
		{
			if (!directionalReceived)
			{
				directionalReceived = true;

				if (Input.GetAxisRaw ("Vertical") > 0)
				{
					if (currentButtonIndex > 0) currentButtonIndex--;
				}
				else if (Input.GetAxisRaw ("Vertical") < 0)
				{
					if (currentButtonIndex < buttons.Length - 1) currentButtonIndex++;
				}
			}
		}
		else if (directionalReceived)
		{
			directionalReceived = false;
		}
	}

	private void Perform_Menu_Function ()
	{
		List<MenuFunctions.MenuFunction> functions = menuFunctions.functions;

		for (int f = 0; f < functions.Count; f++)
		{
			if (f == currentButtonIndex) functions[f] ();
		}
	}
}
