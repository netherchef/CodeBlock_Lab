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

	public float duration;
	public float power;

	// Enumerators

	private Coroutine coroutine_Shake;

	// Debug

	[Header ("Debug:")]
	public bool debug;
	public bool start;

	public float debugDuration = 1f;
	public float debugPower = 1f;
	public float debugLossRate;

	private void Update ()
	{
		if (start)
		{
			start = false;

			Start_Shake (debugDuration, debugPower, debugLossRate);
		}
	}

	private IEnumerator Do_Shake (float dur, float pow, float lossRate)
	{
		// Debug

		if (debug) Debug.Log ("Shaking start.");

		// Set Shaking Bool for external checks

		shaking = true;

		// Record Target's Initial Position

		Vector3 initPos = new Vector3 (0, 0, target.localPosition.z);

		power = pow;
		duration = dur;

		while (duration > 0 && power > 0)
		{
			duration -= Time.deltaTime;

			// Calculate Power Loss

			if (power > 0) power -= lossRate;

			// Calculate Displacement

			Vector3 displacement = Random.insideUnitCircle * power;

			displacement.z = target.localPosition.z;

			// Position Target

			target.localPosition = displacement;

			yield return null;
		}

		// Reset Target Position

		target.localPosition = initPos;

		duration = 0;
		power = 0;

		// Reset Shaking Bool

		shaking = false;

		// Debug

		if (debug) Debug.Log ("Shaking done.");

		// Clear coroutine container

		coroutine_Shake = null;
	}

	public void Start_Shake (float dur = 1f, float pow = 1f, float lossRate = 0f)
	{
		// Start

		if (coroutine_Shake == null)
		{
			coroutine_Shake = StartCoroutine (Do_Shake (dur, pow, lossRate));

			return;
		}

		// Restart

		StopCoroutine (coroutine_Shake);

		coroutine_Shake = StartCoroutine (Do_Shake (dur, pow, lossRate));
	}
}
