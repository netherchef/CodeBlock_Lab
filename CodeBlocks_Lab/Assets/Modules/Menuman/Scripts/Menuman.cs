// Menuman v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menuman : MonoBehaviour
{
	// Components

	public GameObject[] buttons;

	// Scripts

	[Header ("Functions:")]
	public MenuFunctions menuFunctions;

	public MenuSounder sounder;

	// Variables

	[Header ("Button Style:")]
	public float dormantAlpha = .5f;
	public float activeAlpha = 1f;

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

			// Sound

			if (sounder) sounder.Play_Select_Sound ();

			return;
		}

		// Navigate

		if (!directionalReceived)
		{
			if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0)
			{
				directionalReceived = true;

				if (Input.GetAxisRaw ("Vertical") > 0)
				{
					if (currentButtonIndex > 0)
					{
						// Style Previous Button

						Style_UnSelected_Button ();

						// Navigate to Next Button

						currentButtonIndex--;

						// Style Current Button

						Style_Selected_Button ();
					}
				}
				else if (Input.GetAxisRaw ("Vertical") < 0)
				{
					if (currentButtonIndex < buttons.Length - 1)
					{
						// Style Previous Button

						Style_UnSelected_Button ();

						// Navigate to Next Button

						currentButtonIndex++;

						// Style Current Button

						Style_Selected_Button ();
					}
				}

				// Sound

				if (sounder) sounder.Play_Hover_Sound ();

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
	}

	#region Styling ____________________________________________________________

	private void Style_Selected_Button ()
	{
		Change_Alpha (buttons[currentButtonIndex].GetComponent<SpriteRenderer> (), activeAlpha);
	}

	private void Style_UnSelected_Button ()
	{
		Change_Alpha (buttons[currentButtonIndex].GetComponent<SpriteRenderer> (), dormantAlpha);
	}

	private void Change_Alpha (SpriteRenderer sr, float targAlpha)
	{
		Color btnColor = sr.color;
		btnColor.a = targAlpha;

		sr.color = btnColor;
	}

	#endregion

	private void Perform_Menu_Function ()
	{
		List<MenuFunctions.MenuFunction> functions = menuFunctions.functions;

		for (int f = 0; f < functions.Count; f++)
		{
			if (f == currentButtonIndex) functions[f] ();
		}
	}
}
