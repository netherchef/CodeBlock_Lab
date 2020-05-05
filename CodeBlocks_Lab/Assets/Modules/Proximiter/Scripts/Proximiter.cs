using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximiter : MonoBehaviour
{
	// Components

	public Transform origin;
	public Transform[] targets;
	public Transform closestTarget;
	public Transform furthestTarget;

	private float smallestDist;
	private float largestDist;

	// Variables

	public bool findClosest;
	public bool findFurthest;

	private void Update ()
	{
		// Find closest target in the array

		if (findClosest)
		{
			findClosest = false;

			Find_Closest ();
		}

		// Find further target in the array

		else if (findFurthest)
		{
			findFurthest = false;

			Find_Furthest ();
		}
	}

	#region Find _______________________________________________________________

	private void Find_Closest ()
	{
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
			float distance = (origin.position - targets[n].position).magnitude;

			if (smallestDist == 0.0f || distance < smallestDist)
			{
				smallestDist = distance;

				closestTarget = targets[n];
			}
		}
	}

	private void Find_Furthest ()
	{
		furthestTarget = null;
		largestDist = 0.0f;

		for (int f = 0; f < targets.Length; f++)
		{
			float distance = (origin.position - targets[f].position).magnitude;

			if (largestDist == 0.0f || distance > largestDist)
			{
				largestDist = distance;

				furthestTarget = targets[f];
			}
		}
	}

	#endregion

	#region Return _____________________________________________________________

	public Transform Closest_Target ()
	{
		// Do a new first before returning the target for latest result

		Find_Closest ();
		return closestTarget;
	}

	public Transform Furthest_Target ()
	{
		Find_Furthest ();
		return furthestTarget;
	}

	#endregion
}
