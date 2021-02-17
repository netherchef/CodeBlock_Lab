using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PointPathHandler : MonoBehaviour
{
	[Header ("Components:")]

	public Transform target;

	[Header ("Scripts:")]

	public PointPath[] paths;

	// Variables

	private float pathRange = 0.25f;

	[Header ("Runtime Editing:")]

	public bool edit;
	public int pathIndex;
	private int previousIndex = 9999;

	public PointPath currEditPath;
	private bool movingPoint;
	private int currEditPoint;
	private bool newPoint;

	[Header ("Debug:")]

	public bool debug;

	private void OnDrawGizmos ()
	{
		if (debug)
		{
			if (paths.Length <= 0) return;

			// Draw Path

			if (edit)
			{
				for (int p = 0; p < paths.Length; p++)
				{
					if (p == pathIndex)
					{
						// Draw Runtime Editing Path

						for (int o = 0; o < paths[p].points.Count; o++)
						{
							Gizmos.color = Color.yellow;
							Gizmos.DrawWireSphere (paths[p].points[o], pathRange * 2);
						}
					}
					else
					{
						// Draw Normal Path

						for (int o = 0; o < paths[p].points.Count; o++)
						{
							Gizmos.color = Color.white;
							Gizmos.DrawWireSphere (paths[p].points[o], pathRange);
						}
					}
				}
			}
			else
			{
				// Draw Normal Path

				for (int p = 0; p < paths.Length; p++)
				{
					for (int o = 0; o < paths[p].points.Count; o++)
					{
						Gizmos.color = Color.white;
						Gizmos.DrawWireSphere (paths[p].points[o], pathRange);
					}
				}
			}
		}
	}

	private void Start ()
	{
		// Set Edges

		if (paths.Length <= 0) return;

		foreach (PointPath path in paths)
		{
			if (path != null)
			{
				if (path.points.Count <= 0)
				{
					path.NoPoint ();
				}
				else
				{
					for (int p = 0; p < path.points.Count; p++)
					{
						if (path.points[p].x < path.leftEdge) path.leftEdge = path.points[p].x;
						else if (path.points[p].y > path.rightEdge) path.rightEdge = path.points[p].y;
					}
				}
			}
		}
	}

	private void Update ()
	{
		// Move X

		Vector3 newPos = target.position;
		newPos.x += 4f * Input.GetAxisRaw ("Horizontal") * Time.deltaTime;
		target.position = newPos;

		// Position Target on Path

		if (paths.Length > 0)
		{
			foreach (PointPath path in paths)
			{
				if (path != null)
				{
					List<Vector3> points = path.points;

					if (target.position.x > path.leftEdge || target.position.x < path.rightEdge)
					{
						for (int i = 0; i < points.Count - 1; i++)
						{
							if (target.position.x > points[i].x && target.position.x < points[i + 1].x)
							{
								Vector3 pathPos = target.position;

								pathPos.y = Y_Value (points, i, pathPos.x);

								target.position = pathPos;

								i = points.Count;
							}
						}
					}
				}
			}
		}

#if UNITY_EDITOR

		// Runtime Path Edit

		if (edit)
		{
			if (previousIndex != pathIndex)
			{
				previousIndex = pathIndex;

				currEditPath = paths[pathIndex];
			}
		}

		if (currEditPath != null)
		{
			if (Input.GetMouseButton (0) || Input.GetMouseButton (1))
			{
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				mousePos.z = 0;

				// Move Existing Point

				if (movingPoint)
				{
					currEditPath.points[currEditPoint] = mousePos;
				}
				else if (Input.GetMouseButtonDown (0))
				{
					for (int p = 0; p < currEditPath.points.Count; p++)
					{
						if (Vector3.Magnitude (currEditPath.points[p] - mousePos) <= pathRange * 2)
						{
							movingPoint = true;
							currEditPoint = p;

							return;
						}
					}
				}
				else if (Input.GetMouseButtonDown (1))
				{
					// New Point

					// Start Point

					if (Vector3.Magnitude (currEditPath.points[0] - mousePos) <= pathRange * 2)
					{
						movingPoint = true;

						List<Vector3> newPoints = new List<Vector3> ();

						newPoints.Add (currEditPath.points[0]);

						foreach (Vector3 point in currEditPath.points)
						{
							newPoints.Add (point);
						}

						currEditPath.points = newPoints;

						currEditPoint = 0;
						newPoint = true;
					}
					else if (Vector3.Magnitude (currEditPath.points[currEditPath.points.Count - 1] - mousePos) <= pathRange * 2)
					{
						// End Point

						movingPoint = true;

						currEditPath.points.Add (currEditPath.points[currEditPath.points.Count - 1]);
						currEditPoint = currEditPath.points.Count - 1;

						newPoint = true;
					}
				}
			}
			else if (movingPoint)
			{
				movingPoint = false;
				currEditPoint = 0;

				if (newPoint) newPoint = false;
			}
		}

#endif
	}

	private float Y_Value (List<Vector3> points, int segmentIndex, float xVal)
	{
		Vector2 point = points[segmentIndex];

		float segVectorY = Segment_Vector (points, segmentIndex).y;

		return point.y + (segVectorY * Mathf.Abs (point.x - xVal));
	}

	private Vector2 Segment_Vector (List<Vector3> points, int segmentIndex)
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