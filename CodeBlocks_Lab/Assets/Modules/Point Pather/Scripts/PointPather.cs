using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PointPather : MonoBehaviour
{
	[Header ("Components:")]

	public Transform target;

	[Header ("Variables:")]

	public List<Vector3> points = new List<Vector3> ();

	private float pathRange = 0.25f;

	private float leftEdge;
	private float rightEdge;

	[Header ("Runtime Editing:")]

	public bool editable;

	private bool editing;
	private int currEditPoint;
	private bool newPoint;

	[Header ("Debug:")]

	public bool debug;

	private void OnDrawGizmos ()
	{
		if (debug)
		{
			if (points.Count <= 0) return;

			Color pointCol = Color.white;
			Color newPointCol = Color.magenta;

			for (int p = 0; p < points.Count; p++)
			{
				if (editable)
				{
					if (p == 0 && currEditPoint == 0)
					{
						if (newPoint) Gizmos.color = newPointCol;
					}
					else if (p == points.Count - 1 && currEditPoint == points.Count - 1)
					{
						if (newPoint) Gizmos.color = newPointCol;
					}
					else
					{
						pointCol += new Color (-0.1f, 0, 0);
						Gizmos.color = pointCol;
					}
				}
				else
				{
					pointCol += new Color (-0.1f, 0, 0);
					Gizmos.color = pointCol;
				}

				Gizmos.DrawWireSphere (points[p], pathRange * 2);
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

		if (editable)
		{
			if (Input.GetMouseButton (0) || Input.GetMouseButton (1))
			{
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				mousePos.z = 0;

				// Move Existing Point

				if (editing)
				{
					points[currEditPoint] = mousePos;

					return;
				}

				if (Input.GetMouseButtonDown (0))
				{
					for (int p = 0; p < points.Count; p++)
					{
						if (Vector3.Magnitude (points[p] - mousePos) <= pathRange * 2)
						{
							editing = true;
							currEditPoint = p;

							return;
						}
					}
				}

				// New Point

				if (Input.GetMouseButtonDown (1))
				{
					if (Vector3.Magnitude (points[0] - mousePos) <= pathRange * 2)
					{
						editing = true;

						List<Vector3> newPoints = new List<Vector3> ();

						newPoints.Add (points[0]);

						foreach (Vector3 point in points)
						{
							newPoints.Add (point);
						}

						points = newPoints;

						currEditPoint = 0;
						newPoint = true;

						return;
					}

					if (Vector3.Magnitude (points[points.Count - 1] - mousePos) <= pathRange * 2)
					{
						editing = true;

						points.Add (points[points.Count - 1]);
						currEditPoint = points.Count - 1;

						newPoint = true;

						return;
					}
				}
			}
			else if (editing)
			{
				editing = false;
				currEditPoint = 0;

				if (newPoint) newPoint = false;
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

			for (int i = 0; i < points.Count - 1; i++)
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