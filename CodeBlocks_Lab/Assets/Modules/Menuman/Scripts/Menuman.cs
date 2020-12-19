// Menuman v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menuman : MonoBehaviour
{
	// Components

	public GameObject[] buttons;

	[Header ("Functions:")]
	public MenuFunctions menuFunctions;

	// Variables

	private bool directionalReceived;

	private int currentButtonIndex;

	[Header ("Input:")]
	public string enterButton = "Submit";

	private void Start ()
	{
		Change_Alpha (buttons[currentButtonIndex].GetComponent<SpriteRenderer> (), 1f);
	}

	private void Update ()
	{
		// Perform Menu Function

		if (Input.GetButtonDown (enterButton))
		{
			Perform_Menu_Function ();

			return;
		}

		// Navigate

		if (!directionalReceived)
		{
			if (Input.GetAxisRaw ("Vertical") != 0)
			{
				directionalReceived = true;

				if (Input.GetAxisRaw ("Vertical") > 0)
				{
					if (currentButtonIndex > 0)
					{
						Change_Alpha (buttons[currentButtonIndex].GetComponent<SpriteRenderer> (), 0.5f);

						currentButtonIndex--;

						Change_Alpha (buttons[currentButtonIndex].GetComponent<SpriteRenderer> (), 1f);
					}
				}
				else if (Input.GetAxisRaw ("Vertical") < 0)
				{
					if (currentButtonIndex < buttons.Length - 1)
					{
						Change_Alpha (buttons[currentButtonIndex].GetComponent<SpriteRenderer> (), 0.5f);

						currentButtonIndex++;

						Change_Alpha (buttons[currentButtonIndex].GetComponent<SpriteRenderer> (), 1f);
					}
				}

				return;
			}
		}
		else
		{
			if (Input.GetAxisRaw ("Vertical") == 0)
			{
				// Reset

				directionalReceived = false;
			}
		}

		//if (Input.GetAxisRaw ("Vertical") != 0)
		//{
		//	if (!directionalReceived)
		//	{
		//		directionalReceived = true;

		//		if (Input.GetAxisRaw ("Vertical") > 0)
		//		{
		//			if (currentButtonIndex > 0) currentButtonIndex--;
		//		}
		//		else if (Input.GetAxisRaw ("Vertical") < 0)
		//		{
		//			if (currentButtonIndex < buttons.Length - 1) currentButtonIndex++;
		//		}
		//	}
		//}
		//else if (directionalReceived)
		//{
		//	directionalReceived = false;
		//}
	}

	private void Change_Alpha (SpriteRenderer sr, float targAlpha)
	{
		Color btnColor = sr.color;
		btnColor.a = targAlpha;

		sr.color = btnColor;
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
