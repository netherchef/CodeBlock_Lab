using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
	// Components

	public Transform follower;
	public Transform target;

	// Variables

	public float speed = 1;

	private void Update ()
	{
		// If the follower is far from the target, 
		// move the follower towards the target.

		if (Vector3.Magnitude (follower.position - target.position) > 1)
		{
			float movementX = -(follower.position.x - target.position.x);
			float movementY = -(follower.position.y - target.position.y);

			follower.Translate (new Vector3 (movementX, movementY, 0) * speed * Time.deltaTime);
		}
	}
}
