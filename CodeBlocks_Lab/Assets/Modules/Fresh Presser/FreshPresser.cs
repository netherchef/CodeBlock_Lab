using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreshPresser : MonoBehaviour
{
	// Variables

	private bool holdingUp;
	private bool holdingDown;
	private bool holdingLeft;
	private bool holdingRight;

	private void Update ()
	{
		// Up

		if (Press_Up ())
		{
			if (!holdingUp) holdingUp = true;
		}
		else
		{
			if (holdingUp) holdingUp = false;
		}

		// Down

		if (Press_Down ())
		{
			if (!holdingDown) holdingDown = true;
		}
		else
		{
			if (holdingDown) holdingDown = false;
		}

		// Left

		if (Press_Left ())
		{
			if (!holdingLeft) holdingLeft = true;
		}
		else
		{
			if (holdingLeft) holdingLeft = false;
		}

		// Right

		if (Press_Right ())
		{
			if (!holdingRight) holdingRight = true;
		}
		else
		{
			if (holdingRight) holdingRight = false;
		}
	}

	private bool Press_Up ()
	{
		return Input.GetAxisRaw ("Vertical") > 0;
	}

	public bool FreshPress_Up ()
	{
		return Press_Up () && !holdingUp;
	}

	private bool Press_Down ()
	{
		return Input.GetAxisRaw ("Vertical") < 0;
	}

	public bool FreshPress_Down ()
	{
		return Press_Down () && !holdingDown;
	}

	private bool Press_Left ()
	{
		return Input.GetAxisRaw ("Horizontal") < 0;
	}

	public bool FreshPress_Left ()
	{
		return Press_Left () && !holdingLeft;
	}

	private bool Press_Right ()
	{
		return Input.GetAxisRaw ("Horizontal") > 0;
	}

	public bool FreshPress_Right ()
	{
		return Press_Right () && !holdingRight;
	}
}
