// Jumper v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
	// Components

	public Rigidbody2D subject;

	// Variables

	public float jumpVelocity = 5;

	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2;

	private void Update ()
	{
		if (Input.GetButtonDown ("Jump"))
		{
			subject.velocity = new Vector2 (subject.velocity.x, jumpVelocity);
		}
	}

	private void FixedUpdate ()
	{
		if (subject.velocity.y < 0)
		{
			subject.velocity += new Vector2 (0, 1) * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}
		else if (subject.velocity.y > 0 && !Input.GetButton ("Jump"))
		{
			subject.velocity += new Vector2 (0, 1) * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
	}
}
