using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OscillateMode { Horizontal, Vertical, Diagonal_01, Diagonal_02, Circle }

public class Oscillator : MonoBehaviour
{
	// Components

	public Transform target;

	// Variables

	public OscillateMode mode;

	public float range = 1f;
	public float speed = 5f;

	public bool oscillate;

	private Vector3 startPos;
	private float distance;
	private Vector3 corePos;

	[SerializeField]
	private Vector3 endPos;
	[SerializeField]
	private bool resetPos;
	[SerializeField]
	private bool move;
	[SerializeField]
	private float moveSpeed = 1f;
	[SerializeField]
	private float archHeight = 2f;

	// Enumerators

	private IEnumerator do_Oscillate;

	private void Start ()
	{
		//if (oscillate) Start_Oscillation ();

		startPos = Vector3.zero;

		corePos = target.position;
	}

	private void Update ()
	{
		if (move)
		{
			Vector3 currPos = corePos;

			if (Vector3.Distance (currPos, endPos) > 0.1f)
			{
				currPos += Vector3.Normalize (endPos - currPos) * moveSpeed * Time.deltaTime;
			}

			//if (endPos.x > startPos.x && currPos.x < endPos.x)
			//{
			//	currPos += Vector3.Normalize (endPos - currPos) * moveSpeed * Time.deltaTime;
			//}
			//else if (endPos.x < startPos.x && currPos.x > endPos.x)
			//{
			//	currPos += Vector3.Normalize (endPos - currPos) * moveSpeed * Time.deltaTime;
			//}

			corePos = currPos;
		}

		if (resetPos)
		{
			resetPos = false;
			corePos = Vector3.zero;
		}

		if (Vector3.Distance (corePos, endPos) > 0)
		{

			// Calculate Jump Arch depending on the core's current position
			// in relation with the distance between the Start and End positions.

			distance = Vector3.Distance (startPos, endPos);
			float amount = (Vector3.Distance (startPos, corePos) / distance) * 3.142f;

			Vector3 tempPos = corePos;

			// Apply Jump Arch modifier

			tempPos.y += Mathf.Sin (amount) * archHeight;

			// Apply the new position to the transform

			target.position = tempPos;
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

				case OscillateMode.Circle:
					pos.x = Mathf.Sin (s);
					pos.y = Mathf.Cos (s);
					break;
			}

			// Oscillation Ranger

			pos *= range;

			// Change Target Position

			target.position = pos;

			yield return null;
		}

		do_Oscillate = null;
	}

	public void Start_Oscillation ()
	{
		if (do_Oscillate == null)
		{
			do_Oscillate = Do_Oscillate ();
			StartCoroutine (do_Oscillate);

			if (!oscillate) oscillate = true;
		}
	}
}
