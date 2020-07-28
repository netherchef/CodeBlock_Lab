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

	public bool freezeX;
	public bool freezeY;

	private void Update ()
	{
		// If the follower is far from the target, 
		// move the follower towards the target.

		if (Vector3.Magnitude (followerObject.position - targetObject.position) > 1)
		{
			Vector3 movement = new Vector3 ();

			if (!freezeX) movement.x = -(followerObject.position.x - targetObject.position.x);
			if (!freezeY) movement.y = -(followerObject.position.y - targetObject.position.y);

			movement.z = zPosition;

			followerObject.Translate (movement * speed * Time.deltaTime);
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

	public void Freeze_X ()
	{
		freezeX = true;
	}

	public void Unfreeze_X ()
	{
		freezeX = false;
	}

	public void Freeze_Y ()
	{
		freezeY = true;
	}

	public void Unfreeze_Y ()
	{
		freezeY = false;
	}
}
