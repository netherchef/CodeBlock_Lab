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

		target.position = PointOnSegment (1, target.position.x);
	}

	private Vector2 PointOnSegment (int segmentIndex, float targetX)
	{
		Vector2 point = new Vector2 (targetX, 0);

		Vector2 ratio = Segment_Ratio (segmentIndex);

		point.y = -(Mathf.Abs (point.x) / ratio.x);

		return point;
	}

	private Vector2 Segment_Ratio (int segmentIndex)
	{
		Vector2 ratio = new Vector2 (0, 0);

		Vector2 pointA = path[segmentIndex - 1];
		Vector2 pointB = path[segmentIndex];

		ratio.x = Mathf.Abs (pointA.x - pointB.x);
		ratio.y = Mathf.Abs (pointA.y - pointB.y);

		float denom = ratio.x < ratio.y ? ratio.x : ratio.y;

		ratio /= denom;

		return ratio;
	}
}
