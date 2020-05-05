using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximiter : MonoBehaviour
{
	// Components

	public Transform player;
	public Transform[] targets;
	public Transform closestTarget;

	private float smallestDist;

	// Variables

	public bool findClosest;

	private void Update ()
	{
		if (findClosest)
		{
			findClosest = false;

			// Clear results from last check

			closestTarget = null;
			smallestDist = 0.0f;

			// Go through all targets in the array.
			// If the smallest distance recorded is 0, meaning it has NOT been changed,
			// assign the current target's distance to it.
			// If the next target's distance is smaller than the last recorded distance,
			// assign the current target's distance to it.

			for (int n = 0; n < targets.Length; n++)
			{
				float distance = (player.position - targets[n].position).magnitude;

				if (smallestDist == 0.0f || distance < smallestDist)
				{
					smallestDist = distance;

					closestTarget = targets[n];
				}
			}
		}
	}
}
