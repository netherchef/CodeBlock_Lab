// Follower v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
	// Components

	public Transform followerObject;
	public Transform targetObject;

	// Variables

	public float speed = 1;

	public float zPosition;

	private void Update ()
	{
		Follow (followerObject, targetObject);
	}

	public void Follow (Transform follower, Transform target)
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

	#region Target _____________________________________________________________

	public void Change_Target (Transform newTarget)
	{
		targetObject = newTarget;
	}

	#endregion

	#region Speed ______________________________________________________________

	public void Change_Speed (float newSpeed)
	{
		speed = newSpeed;
	}

	#endregion
}
