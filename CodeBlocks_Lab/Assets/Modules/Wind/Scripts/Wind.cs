// Wind v0.013

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gust
{
	public Vector3 position;
	public float duration;
	public float radius;
	public float strength;
	public bool blowRight;
}

public class Wind : MonoBehaviour
{
	// Components

	public Transform[] targets;
	public List<Gust> gusts;

	// Variables

	public float entropy = 1;

	public bool spawnGust;

	// Debug

	public bool debug;

	private void OnDrawGizmos ()
	{
		if (debug)
		{
			for (int g = 0; g < gusts.Count; g++)
			{
				Gizmos.DrawWireSphere (gusts[g].position, gusts[g].radius);
			}
		}
	}

	private void Update ()
	{
		// Start by decreasing every gust's duration.
		// If it is depleted remove it and return. If NOT, push targets.

		for (int g = 0; g < gusts.Count; g++)
		{
			// Gust Duration

			if (gusts[g].duration <= 0)
			{
				gusts.Remove (gusts[g]);

				return;
			}

			gusts[g].duration -= entropy * Time.deltaTime;

			// Push Targets

			for (int t = 0; t < targets.Length; t++)
			{
				// If the target is within range of the gust, push the target.

				if (Vector3.Magnitude (targets[t].position - gusts[g].position) <= gusts[g].radius)
				{
					// Set wind direction

					int direction = gusts[g].blowRight ? 1 : -1;

					// Add force to rigidbody

					targets[t].GetComponent<Rigidbody2D> ().AddForce (new Vector2 (gusts[g].strength * direction, 0));
				}
			}
		}

		// Spawn Gust

		if (spawnGust)
		{
			spawnGust = false;

			Spawn_Gust (
				new Vector2 (Random.Range (-4, 4), Random.Range (2, 4)), // Position
				5, // Duration
				Random.Range (2, 3), // Radius
				Random.Range (1, 4), // Strength
				true // Blowing Right
				);
		}
	}

	#region Spawn Gust _________________________________________________________

	private void Spawn_Gust (Vector2 pos, float dur, float rad, float str, bool bRight)
	{
		Gust gust = new Gust { position = pos, duration = dur, radius = rad, strength = str, blowRight = bRight };

		gusts.Add (gust);
	}

	#endregion
}
