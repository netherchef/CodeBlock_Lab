// Puzzler v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PuzzleType { LockAndKey, AppleInABasket }

public class Puzzler : MonoBehaviour
{
	// Components

	public PuzzleType puzzleType;

	// Apples In A Basket

	[Header ("Apples In A Basket")]

	public List<Transform> apples;
	public Transform basket;

	public float basketRange = 1;

	// Lock And Key

	[Header ("Lock And Key")]

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
			case PuzzleType.AppleInABasket:
				Start_ApplesInABasket ();
				break;
		}
	}

	private IEnumerator ApplesInABasket ()
	{
		// While there are apples outside the basket, check the distance between
		// each apple and the basket.

		bool applesFound = false;

		while (!applesFound)
		{
			for (int a = 0; a < apples.Count; a++)
			{
				if (Vector3.Magnitude (apples[a].position - basket.position) < basketRange)
				{
					// If an apple is close to the basket,
					// disable it and remove it from the list.

					apples[a].gameObject.SetActive (false);
					apples.Remove (apples[a]);

					// If there are no more apples to be found, end the puzzle.

					if (apples.Count <= 0) applesFound = true;
				}
			}

			yield return null;
		}

		// The puzzle has ended

		End_ApplesInABasket ();
	}

	private void Start_ApplesInABasket ()
	{
		StartCoroutine ("ApplesInABasket");
	}

	private void End_ApplesInABasket ()
	{
		print ("The apples are in.");
	}

	#region Lock And Key _______________________________________________________

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

	#endregion
}