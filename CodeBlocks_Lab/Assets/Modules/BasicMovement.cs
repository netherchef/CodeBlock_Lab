using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
	public Transform master;

	public float speed = 4f;

	private void Update ()
	{
		float xInput = Input.GetAxisRaw ("Horizontal");
		float yInput = Input.GetAxisRaw ("Vertical");

		if (Mathf.Abs(xInput) > Mathf.Epsilon)
		{
			master.position += new Vector3 (Mathf.Sign (xInput), 0) * speed * Time.deltaTime;
		}

		if (Mathf.Abs (yInput) > Mathf.Epsilon)
		{
			master.position += new Vector3 (0, Mathf.Sin (yInput)) * speed * Time.deltaTime;
		}
	}
}
