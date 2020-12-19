using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
	// Components

	public Transform targetContainer;
	public Transform target;

	// Variables

	public bool shaking;

	// Enumerators

	private IEnumerator do_Shake;

	// Debug

	[Header ("Debug:")]
	public bool debug;
	public bool start;

	public float debugDuration = 1f;
	public float debugPower = 1f;
	public float debugLossRate = 0f;

	private void Update ()
	{
		if (start)
		{
			start = false;

			Start_Shake (debugDuration, debugPower, debugLossRate);
		}
	}

	private IEnumerator Do_Shake (float duration = 1f, float power = 1f, float lossRate = 0f)
	{
		shaking = true;

		float pow = power;

		float endTime = Time.time + duration;

		while (Time.time < endTime && pow > 0)
		{
			// Calculate Power Loss

			if (pow > 0) pow -= lossRate;

			// Calculate Displacement

			Vector3 displacement = Random.insideUnitCircle * pow;

			// Determine Final Position

			Vector3 finalPos = new Vector3 (targetContainer.position.x + displacement.x, targetContainer.position.y + displacement.y, target.position.z);

			// Position Target at Final Position

			target.position = finalPos;

			yield return null;
		}

		// Reset Target Position

		target.position = new Vector3 (targetContainer.position.x, targetContainer.position.y, target.position.z);

		// Reset

		do_Shake = null;

		shaking = false;
	}

	public void Start_Shake (float duration = 1f, float power = 1f, float lossRate = 0f)
	{
		if (shaking)
		{
			if (debug) Debug.LogWarning ("Do Shake has NOT been cleared.");

			return;
		}

		do_Shake = Do_Shake (duration, power, lossRate);
		StartCoroutine (do_Shake);
	}
}
