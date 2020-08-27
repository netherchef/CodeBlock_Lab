using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MenuButton
{
	public Transform transform;
	public Collider2D collider;
}

public class MenuRayman : MonoBehaviour
{
	// Components

	public MenuFunctions menuFunctions;
	public MenuStyler menuStyler;

	public Transform firstButton;
	private MenuButton currentButton;

	// Variables

	public float distance = 1f;

	public string currentFunction;

	private bool holdingUp;
	private bool holdingDown;

	// Debug

	public bool debug;

	private void OnEnable ()
	{
		// Assign the current button to begin with.

		currentButton.transform = firstButton;
		currentButton.collider = firstButton.GetComponent<Collider2D> ();

		// Set current function

		currentFunction = currentButton.transform.name;

		// Select first button

		menuStyler.Select (currentButton);
	}

	private void Update ()
	{
		// Debug

		Debug_Ray ();

		// Run Function

		if (Press_Submit ())
		{
			Run_Function (currentFunction);

			return;
		}

		// Get next function

		if (Press_Up ())
		{
			if (!holdingUp)
			{
				holdingUp = true;

				Get_Next_Btn ();
			}

			return;
		}

		if (Press_Down ())
		{
			if (!holdingDown)
			{
				holdingDown = true;

				Get_Next_Btn ();
			}

			return;
		}

		// If there's no input

		if (holdingUp) holdingUp = false;
		if (holdingDown) holdingDown = false;
	}

	#region Find Next Button ___________________________________________________

	private void Get_Next_Btn ()
	{
		RaycastHit2D nextBtn = Next_Button (currentButton.transform);

		if (nextBtn)
		{
			// Try to run the function.

			string nextFunction = nextBtn.transform.name;

			if (nextFunction != "")
			{
				currentFunction = nextFunction;
			}

			// Enable the current button's collider and
			// apply its UNselected styling.

			currentButton.collider.enabled = true;
			menuStyler.UnSelect (currentButton);

			// Assign the new current button.

			currentButton.transform = nextBtn.transform;
			currentButton.collider = nextBtn.collider;

			// Disable the next button's collider to
			// prevent the raycast from hitting it.
			// Also apply its selected styling.

			currentButton.collider.enabled = false;
			menuStyler.Select (currentButton);
		}
		else
		{
			// Debug

			if (debug) Debug.LogWarning ("No button found.");
		}
	}

	private RaycastHit2D Next_Button (Transform currBtn)
	{
		RaycastHit2D hit = Physics2D.Raycast (currBtn.position, Direction (), distance, LayerMask.GetMask ("Menu"));

		return hit;
	}

	private Vector3 Direction ()
	{
		Vector3 d = new Vector3 (0, 0, 0);

		if (Input.GetAxisRaw ("Vertical") > 0)
		{
			d = new Vector3 (0, 1, 0);
		}
		else if (Input.GetAxisRaw ("Vertical") < 0)
		{
			d = new Vector3 (0, -1, 0);
		}

		return d;
	}

	#endregion

	#region Run Function _______________________________________________________

	private void Run_Function (string funcName)
	{
		menuFunctions.Run_Function (funcName);
	}

	#endregion

	#region Input ______________________________________________________________

	private bool Press_Submit ()
	{
		return Input.GetButtonDown ("Submit");
	}

	private bool Press_Up ()
	{
		return Direction ().y > 0;
	}

	private bool Press_Down ()
	{
		return Direction ().y < 0;
	}

	#endregion

	#region Debug ______________________________________________________________

	private void Debug_Ray ()
	{
		if (debug)
		{
			Debug.DrawLine (currentButton.transform.position, currentButton.transform.position + Direction () * distance, Color.yellow);
		}
	}

	#endregion
}
