using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPather : MonoBehaviour
{
	[Header ("Components:")]

	public Transform target;

	[Header ("Variables:")]

	//public Vector3 leftEdge;
	//public Vector3 rightEdge;

	[Space (10)]

	public Vector3[] path;

	[Header ("Debug:")]

	public bool debug;

	public bool snapTarget;

	private void OnDrawGizmos ()
	{
		if (debug)
		{
			foreach (Vector3 point in path)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawCube (point, new Vector3 (1, 1, 1));
			}
		}
	}

	//private void Start ()
	//{
	//	foreach (Vector3 point in path)
	//	{
	//		if (point.x < leftEdge.x) leftEdge = point;
	//		else if (point.y > rightEdge.x) rightEdge = point;
	//	}
	//}

	private void Update ()
	{
		//if (snapTarget)
		//{
		//	snapTarget = false;

		//	target.position = PointOnSegment (1, target.position.x);
		//}

		Vector3 newPos = target.position;

		newPos.y = Y_Value (1, newPos.x);

		target.position = newPos;

		//target.position = PointOnSegment (1, target.position.x);
	}

	private Vector2 PointOnSegment (int segmentIndex, float targetX)
	{
		Vector2 newPoint = new Vector2 (targetX, 0);



		//Vector2 ratio = Segment_Ratio (segmentIndex);

		//newPoint.y = -(Mathf.Abs (newPoint.x) / ratio.x);

		return newPoint;
	}

	private float Y_Value (int segmentIndex, float xVal)
	{
		float y = 0;

		Vector2 point = path[segmentIndex];

		float segVectorY = Segment_Vector (segmentIndex).y;

		y = point.y + -(segVectorY * Mathf.Abs (point.x - xVal));

		return y;
	}

	private Vector2 Segment_Vector (int segmentIndex)
	{
		Vector2 segVector = new Vector2 (0, 0);

		Vector2 pointA = path[segmentIndex - 1];
		Vector2 pointB = path[segmentIndex];

		segVector.x = -(pointA.x - pointB.x);
		segVector.y = -(pointA.y - pointB.y);

		float denom = Mathf.Abs (segVector.x) > Mathf.Abs (segVector.y) ? segVector.x : segVector.y;

		segVector /= Mathf.Abs (denom);

		return segVector;
	}

	//private Vector2 Segment_Ratio (int segmentIndex)
	//{
	//	Vector2 ratio = new Vector2 (0, 0);

	//	Vector2 pointA = path[segmentIndex - 1];
	//	Vector2 pointB = path[segmentIndex];

	//	ratio.x = Mathf.Abs (pointA.x - pointB.x);
	//	ratio.y = Mathf.Abs (pointA.y - pointB.y);

	//	float denom = ratio.x > ratio.y ? ratio.x : ratio.y;

	//	ratio /= denom;

	//	return ratio;
	//}
}
