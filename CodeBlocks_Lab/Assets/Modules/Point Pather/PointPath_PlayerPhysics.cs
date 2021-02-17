using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPath_PlayerPhysics : MonoBehaviour
{
	private Rigidbody2D rb2d;

	public bool landed;

	private void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	private void Update ()
	{
		if (Input.GetButtonDown ("Jump"))
		{
			if (landed) Jump ();
		}
	}

	private void Jump ()
	{
		Fall ();

		rb2d.AddForce (Vector2.up * 250f);
	}

	public void Land ()
	{
		Vector3 velo = rb2d.velocity;
		velo.y = 0;
		rb2d.velocity = velo;

		rb2d.isKinematic = true;

		landed = true;
	}

	public void Fall ()
	{
		rb2d.isKinematic = false;

		landed = false;
	}

	public bool Is_Falling ()
	{
		return rb2d.velocity.y < 0;
	}

	public bool Is_Jumping ()
	{
		return Input.GetButtonDown ("Jump");
	}
}
