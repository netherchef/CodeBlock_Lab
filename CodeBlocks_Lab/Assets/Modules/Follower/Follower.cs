// Follower v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
	// Components

	public Transform follower;
	public Transform target;

	private Transform initialTarget;

	// Variables

	public float speed = 1;

	public float zPosition;

	private void Start ()
	{
		// Save initial target for target changing.

		initialTarget = target;
	}

	private void Update ()
	{
		// If the follower is far from the target, 
		// move the follower towards the target.

		if (Vector3.Magnitude (follower.position - target.position) > 1)
		{
			float x = -(follower.position.x - target.position.x);
			float y = -(follower.position.y - target.position.y);

			follower.Translate (new Vector3 (x, y, zPosition) * speed * Time.deltaTime);
		}
	}

	private void Change_Target (Transform newTarget)
	{
		target = newTarget;
	}

	private void Reset_Target ()
	{
		Change_Target (initialTarget);
	}
}
