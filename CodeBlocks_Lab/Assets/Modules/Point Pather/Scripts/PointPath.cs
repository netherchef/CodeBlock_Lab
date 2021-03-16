using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPath : MonoBehaviour
{
	[Header ("Edges:")]

	public float leftEdge;
	public float rightEdge;

	[Header ("Points:")]

	public List<Vector3> points = new List<Vector3> ();

	public void NoPoint ()
	{
		Debug.LogWarning ("Path: [" + transform.name + "] has NO Points.");
	}
}