using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OscillateMode { Horizontal, Vertical, Diagonal_01, Diagonal_02 }

public class Oscillator : MonoBehaviour
{
	// Components

	public Transform target;

	// Variables

	public OscillateMode mode;

	public float range = 1f;
	public float speed = 5f;

	public bool oscillate;

	// Enumerators

	private IEnumerator do_Oscillate;

	private void Start ()
	{
		if (oscillate)
		{
			do_Oscillate = Do_Oscillate ();
			StartCoroutine (do_Oscillate);
		}
	}

	private IEnumerator Do_Oscillate ()
	{
		Vector3 initPos = target.localPosition;

		while (oscillate)
		{
			// Oscillation Speed

			float s = Time.time * speed;

			// Determine Positions

			Vector3 pos = initPos;

			switch (mode)
			{
				case OscillateMode.Horizontal:
					pos.x = Mathf.Cos (s);
					break;

				case OscillateMode.Vertical:
					pos.y = Mathf.Cos (s);
					break;

				case OscillateMode.Diagonal_01:
					pos.x = -Mathf.Cos (s);
					pos.y = Mathf.Cos (s);
					break;

				case OscillateMode.Diagonal_02:
					pos.x = Mathf.Cos (s);
					pos.y = Mathf.Cos (s);
					break;
			}

			// Oscillation Ranger

			pos *= range;

			// Change Target Position

			target.position = pos;

			yield return null;
		}
	}
}
