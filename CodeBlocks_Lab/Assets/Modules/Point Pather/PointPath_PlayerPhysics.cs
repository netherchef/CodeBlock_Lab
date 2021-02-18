using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPath_PlayerPhysics : MonoBehaviour
{
	// Components

	private Rigidbody2D rb2d;

	[Header ("Variables:")]

	public bool grounded;

	//[Header ("Debug:")]

	//public bool debug;

	private void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	private void Update ()
	{
		if (Press_Jump ())
		{
			if (grounded) Jump ();
		}
	}

	public void Jump ()
	{
		UnGround ();

		Vector3 newV = rb2d.velocity;
		newV.y = 5f;
		rb2d.velocity = newV;
	}

	public void Land ()
	{
		Vector3 velo = rb2d.velocity;
		velo.y = 0;
		rb2d.velocity = velo;

		rb2d.isKinematic = true;

		grounded = true;
	}

	public void UnGround ()
	{
		rb2d.isKinematic = false;

		grounded = false;
	}

	public bool Is_Falling ()
	{
		return rb2d.velocity.y < 0;
	}

	public bool Is_Rising ()
	{
		return rb2d.velocity.y > 0;
	}

	public float VelocityY ()
	{
		return rb2d.velocity.y;
	}

	public bool Press_Jump ()
	{
		return Input.GetButtonDown ("Jump");
	}
}
