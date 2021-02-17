using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPath : MonoBehaviour
{
	public string pathName = "PATHNAME";

	[Header ("Edges:")]

	public float leftEdge;
	public float rightEdge;

	[Header ("Points:")]

	public List<Vector3> points = new List<Vector3> ();

	public void NoPoint ()
	{
		if (pathName != "")
		{
			Debug.LogWarning ("Path: [" + pathName + "] has NO Points.");
		}
	}
}
