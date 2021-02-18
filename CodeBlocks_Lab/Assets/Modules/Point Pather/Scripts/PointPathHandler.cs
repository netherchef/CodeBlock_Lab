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
	public int editIndex;
	private int previousIndex = 9999;

	public PointPath currEditPath;
	private bool movingPoint;
	private int currEditPoint;
	private bool newPoint;

	[Header ("Debug:")]

	public bool debugPath;
	public bool debugPhysics;

	public PointPath activePath;

	private void OnDrawGizmos ()
	{
		if (debugPath)
		{
			if (paths.Length <= 0) return;

			// Draw Path

			if (edit)
			{
				for (int p = 0; p < paths.Length; p++)
				{
					if (p == editIndex)
					{
						// Draw Runtime Editing Path

						for (int o = 0; o < paths[p].points.Count; o++)
						{
							Gizmos.color = Color.yellow;
							Gizmos.DrawWireSphere (paths[p].points[o], pathRange);

							if (o < paths[p].points.Count - 1)
							{
								Debug.DrawLine (paths[p].points[o], paths[p].points[o + 1], Color.white);
							}
						}
					}
					else
					{
						// Draw Normal Path

						for (int o = 0; o < paths[p].points.Count; o++)
						{
							Gizmos.color = Color.white;
							Gizmos.DrawWireSphere (paths[p].points[o], pathRange);

							if (o < paths[p].points.Count - 1)
							{
								Debug.DrawLine (paths[p].points[o], paths[p].points[o + 1], Color.white);
							}
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
						if (paths[p].points[o].x == paths[p].leftEdge)
						{
							Gizmos.color = Color.green;
							Gizmos.DrawWireSphere (paths[p].points[o], pathRange);
						}
						else if (paths[p].points[o].x == paths[p].rightEdge)
						{
							Gizmos.color = Color.red;
							Gizmos.DrawWireSphere (paths[p].points[o], pathRange);
						}
						else
						{
							Gizmos.color = Color.white;
							Gizmos.DrawWireSphere (paths[p].points[o], pathRange);
						}

						if (o < paths[p].points.Count - 1)
						{
							Debug.DrawLine (paths[p].points[o], paths[p].points[o + 1], Color.white);
						}
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
			bool leftSet = false;
			bool rightSet = false;

			if (path != null)
			{
				if (path.points.Count <= 0)
				{
					path.NoPoint ();
				}
				else
				{
					for (int o = 0; o < path.points.Count; o++)
					{
						if (!leftSet)
						{
							path.leftEdge = path.points[o].x;

							leftSet = true;
						}
						else
						{
							if (path.points[o].x < path.leftEdge)
							{
								path.leftEdge = path.points[o].x;
							}
						}

						if (!rightSet)
						{
							path.rightEdge = path.points[o].x;

							rightSet = true;
						}
						else
						{
							if (path.points[o].x > path.rightEdge)
							{
								path.rightEdge = path.points[o].x;
							}
						}
					}
				}
			}
		}
	}

	private void FixedUpdate ()
	{
		// Move X

		Vector3 newPos = target.position;
		newPos.x += 4f * Input.GetAxisRaw ("Horizontal") * Time.deltaTime;
		target.position = newPos;

		PointPath_PlayerPhysics targPhysics = target.GetComponent<PointPath_PlayerPhysics> ();

		// Get Active Path

		bool pathFound = false;

		for (int p = 0; p < paths.Length; p++)
		{
			PointPath currPath = paths[p];

			if (target.position.x > currPath.leftEdge && target.position.x < currPath.rightEdge)
			{
				for (int o = 0; o < currPath.points.Count; o++)
				{
					if (target.position.x > currPath.points[o].x && target.position.x < currPath.points[o + 1].x)
					{
						newPos.y = Y_Value (currPath.points, o, newPos.x);

						if (Mathf.Abs (target.position.y - newPos.y) <= pathRange)
						{
							pathFound = true;

							activePath = paths[p];

							o = currPath.points.Count;
							p = paths.Length;
						}
					}
				}
			}
		}

		if (!pathFound)
		{
			if (targPhysics.grounded) targPhysics.UnGround ();

			if (activePath) activePath = null;

			return;
		}

		// Position Target on Path

		if (targPhysics.grounded)
		{
			target.position = newPos;
		}
		else if (!targPhysics.grounded)
		{
			if (targPhysics.Is_Rising ()) return;

			if (debugPhysics)
			{
				Debug.Log ("Ground | Y Velocity: " + targPhysics.VelocityY ());
			}

			targPhysics.Land ();

			target.position = newPos;
		}

		//int activePathIndex = 0;
		//bool pathFound = false;

		//for (int p = 0; p < paths.Length; p++)
		//{
		//	if (target.position.x > paths[p].leftEdge && target.position.x < paths[p].rightEdge)
		//	{
		//		activePathIndex = p;

		//		p = paths.Length;

		//		pathFound = true;
		//	}
		//}

		//if (!pathFound)
		//{
		//	if (targPhysics.landed) targPhysics.Fall ();

		//	return;
		//}

		//// Position Target on Path

		//List<Vector3> points = paths[activePathIndex].points;

		//for (int i = 0; i < points.Count - 1; i++)
		//{
		//	// Choose Segment

		//	if (target.position.x > points[i].x && target.position.x < points[i + 1].x)
		//	{
		//		Vector3 pathPos = target.position;

		//		if (targPhysics.Is_Falling ())
		//		{
		//			pathPos.y = Y_Value (points, i, pathPos.x);

		//			if (Mathf.Abs (target.position.y - pathPos.y) <= pathRange)
		//			{
		//				target.position = pathPos;

		//				targPhysics.Land ();
		//			}

		//			return;
		//		}

		//		if (targPhysics.Is_Jumping ())
		//		{
		//			return;
		//		}
		//		else if (targPhysics.landed)
		//		{
		//			pathPos.y = Y_Value (points, i, pathPos.x);

		//			target.position = pathPos;

		//			return;
		//		}
		//	}
		//}
	}

	private void Update ()
	{
		// Runtime Path Edit

		if (!edit) return;

		// Path Selection for Runtime Edit

		if (previousIndex != editIndex)
		{
			previousIndex = editIndex;

			currEditPath = paths[editIndex];
		}

		if (currEditPath != null)
		{
			// Update Edges

			for (int o = 0; o < currEditPath.points.Count; o++)
			{
				if (currEditPath.points[o].x < currEditPath.leftEdge)
				{
					currEditPath.leftEdge = currEditPath.points[o].x;
				}

				if (currEditPath.points[o].x > currEditPath.rightEdge)
				{
					currEditPath.rightEdge = currEditPath.points[o].x;
				}
			}

			// Edit Points

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
						if (Vector3.Magnitude (currEditPath.points[p] - mousePos) <= pathRange)
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

					if (Vector3.Magnitude (currEditPath.points[0] - mousePos) <= pathRange)
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
					else if (Vector3.Magnitude (currEditPath.points[currEditPath.points.Count - 1] - mousePos) <= pathRange)
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