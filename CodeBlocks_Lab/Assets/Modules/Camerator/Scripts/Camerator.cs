// Cameratir v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraEventType { Zone, Zone_Wait }

public class Camerator : MonoBehaviour
{
	// Components

	public CameraEventType cameraEventType;

	public Follower follower;

	public Transform player;

	public Transform specialCameraTarget;

	// Variables

	public float specialCameraZoneSize = 1;

	public bool cameraEventRunning;

	private void Update ()
	{
		if (!cameraEventRunning)
		{
			switch (cameraEventType)
			{
				case CameraEventType.Zone:
					Check_CamEvent_Zone ();
					break;
				case CameraEventType.Zone_Wait:
					Check_CamEvent_ZoneWait ();
					break;
			}
		}
	}

	#region Camera Event - Zone ________________________________________________

	private void Check_CamEvent_Zone ()
	{
		// If the player camera event zone, start Zone.

		if (Vector3.Magnitude (player.position - specialCameraTarget.position) < specialCameraZoneSize)
		{
			Start_CamEvent_Zone ();
		}
	}

	private void Start_CamEvent_Zone ()
	{
		StartCoroutine ("CamEvent_Zone_EntryHandler");
	}

	private IEnumerator CamEvent_Zone_EntryHandler ()
	{
		// Prevent other camera events starting

		cameraEventRunning = true;

		yield return CamEvent_Zone ();

		// Allow camera events to start

		cameraEventRunning = false;
	}

	private IEnumerator CamEvent_Zone ()
	{
		// Target the special target.
		// Wait for the player to exit the event zone, then target the player.

		follower.Change_Target (specialCameraTarget);

		while (Vector3.Magnitude (player.position - specialCameraTarget.position) < specialCameraZoneSize)
		{
			yield return null;
		}

		follower.Change_Target (player);
	}

	#endregion

	#region Camera Event - Zone Wait ___________________________________________

	private void Check_CamEvent_ZoneWait ()
	{
		// If the player camera event zone, start Zone.

		if (Vector3.Magnitude (player.position - specialCameraTarget.position) < specialCameraZoneSize)
		{
			Start_CamEvent_ZoneWait ();
		}
	}

	private void Start_CamEvent_ZoneWait ()
	{
		StartCoroutine ("CamEvent_ZoneWait_EntryHandler");
	}

	private IEnumerator CamEvent_ZoneWait_EntryHandler ()
	{
		// Prevent other camera events starting

		cameraEventRunning = true;

		yield return CamEvent_ZoneWait ();

		while (Vector3.Magnitude (player.position - specialCameraTarget.position) < specialCameraZoneSize)
		{
			yield return null;
		}

		// Allow camera events to start

		cameraEventRunning = false;
	}

	private IEnumerator CamEvent_ZoneWait ()
	{
		// Target the special target.
		// Wait for a specific input, then target the player.

		follower.Change_Target (specialCameraTarget);

		while (!Input_Received ()) yield return null;

		follower.Change_Target (player);
	}

	#endregion

	private bool Input_Received ()
	{
		return Input.GetButtonDown ("Submit");
	}
}
