using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	// Components

	public Transform subject;

	// Variables

	public float speed = 1;

	private void Update ()
	{
		if (Input.GetAxisRaw ("Horizontal") != 0)
		{
			subject.Translate (new Vector2 (Mathf.Sign (Input.GetAxisRaw ("Horizontal")), 0) * speed * Time.deltaTime);
		}
	}
}
