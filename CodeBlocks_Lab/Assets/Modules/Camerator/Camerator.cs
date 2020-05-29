using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerator : MonoBehaviour
{
	// Components

	public Follower follower;

	public Transform player;

	public Transform specialCameraTarget;

	// Variables

	public float specialCameraZoneSize = 1;

	public bool cameraEventRunning;

	private void Update ()
	{
		// If there is NO camera event running,
		// wait for the player to enter camera event zone.

		if (!cameraEventRunning)
		{
			// Once the player enters the zone, make the camera target the special target.

			if (Vector3.Magnitude (player.position - specialCameraTarget.position) < specialCameraZoneSize)
			{
				cameraEventRunning = true;

				follower.Change_Target (specialCameraTarget);
			}

			return;
		}

		// If there is a camera event running,
		// wait for the player to exit the camera event zone.

		if (Vector3.Magnitude (player.position - specialCameraTarget.position) > specialCameraZoneSize)
		{
			// Once the player exits the zone, make the camera target the player.

			cameraEventRunning = false;

			follower.Change_Target (player);
		}
	}
}
