// Puzzler v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PuzzleType { LockAndKey }

public class Puzzler : MonoBehaviour
{
	// Components

	public PuzzleType puzzleType;

	public Transform Lock;
	public Transform Key;

	public float range = 1;

	private void Start ()
	{
		switch (puzzleType)
		{
			case PuzzleType.LockAndKey:
				Start_LockAndKey ();
				break;
		}
	}

	private IEnumerator LockAndKey ()
	{
		// While the lock and key are NOT close, do whatever

		while (Vector3.Magnitude (Lock.position - Key.position) > range)
		{
			print (Vector3.Magnitude (Lock.position - Key.position));

			yield return null;
		}

		// The puzzle has ended

		End_LockAndKey ();
	}

	private void Start_LockAndKey ()
	{
		StartCoroutine ("LockAndKey");
	}

	private void End_LockAndKey ()
	{
		print ("We're done here.");
	}
}