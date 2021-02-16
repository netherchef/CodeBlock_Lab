using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PointPather : MonoBehaviour
{
	[Header ("Components:")]

	public Transform target;

	[Header ("Variables:")]

	public Vector3[] points;

	private float pathRange = 0.25f;

	private float leftEdge;
	private float rightEdge;

	[Header ("Debug:")]

	public bool debug;

	public bool editted;
	private bool editing;
	private int editPoint;

	private void OnDrawGizmos ()
	{
		if (debug)
		{
			if (points.Length <= 0) return;

			Color pointCol = Color.white;

			foreach (Vector3 point in points)
			{
				pointCol += new Color (-0.1f, 0, 0);
				Gizmos.color = pointCol;
				Gizmos.DrawWireSphere (point, pathRange);
			}
		}
	}

	private void Start ()
	{
		foreach (Vector3 point in points)
		{
			if (point.x < leftEdge) leftEdge = point.x;
			else if (point.y > rightEdge) rightEdge = point.x;
		}
	}

	private void Update ()
	{
		// Runtime Path Edit

		if (debug)
		{
			if (Input.GetMouseButton (0))
			{
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				mousePos.z = 0;

				if (editing)
				{
					points[editPoint] = mousePos;
				}
				else if (Input.GetMouseButtonDown (0))
				{
					for (int p = 0; p < points.Length; p++)
					{
						if (Vector3.Magnitude (points[p] - mousePos) <= pathRange)
						{
							editing = true;
							editPoint = p;

							if (!editted) editted = true;
						}
					}
				}
			}
			else if (editing)
			{
				editing = false;
				editPoint = 0;
			}
		}

		// Move X

		Vector3 newPos = target.position;
		newPos.x += 4f * Input.GetAxisRaw ("Horizontal") * Time.deltaTime;
		target.position = newPos;

		// Check Path

		if (target.position.x > leftEdge || target.position.x < rightEdge)
		{
			// Position on Path

			for (int i = 0; i < points.Length - 1; i++)
			{
				if (target.position.x > points[i].x && target.position.x < points[i + 1].x)
				{
					Vector3 pathPos = target.position;

					pathPos.y = Y_Value (i, pathPos.x);

					target.position = pathPos;

					return;
				}
			}
		}
	}

	private float Y_Value (int segmentIndex, float xVal)
	{
		Vector2 point = points[segmentIndex];

		float segVectorY = Segment_Vector (segmentIndex).y;

		return point.y + (segVectorY * Mathf.Abs (point.x - xVal));
	}

	private Vector2 Segment_Vector (int segmentIndex)
	{
		Vector2 segVector = new Vector2 (0, 0);

		Vector2 pointA = points[segmentIndex];
		Vector2 pointB = points[segmentIndex + 1];

		segVector.x = -(pointA.x - pointB.x);
		segVector.y = -(pointA.y - pointB.y);

		float denom = Mathf.Abs (segVector.x) > Mathf.Abs (segVector.y) ? segVector.x : segVector.y;

		segVector /= Mathf.Abs (denom);

		return segVector;
	}
}
