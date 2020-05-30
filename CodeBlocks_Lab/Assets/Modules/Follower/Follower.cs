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
		// If the follower is far from the target, 
		// move the follower towards the target.

		if (Vector3.Magnitude (followerObject.position - targetObject.position) > 1)
		{
			float x = -(followerObject.position.x - targetObject.position.x);
			float y = -(followerObject.position.y - targetObject.position.y);

			followerObject.Translate (new Vector3 (x, y, zPosition) * speed * Time.deltaTime);
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
