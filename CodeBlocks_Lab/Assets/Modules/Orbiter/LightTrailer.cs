using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode { SinSin, SinCos, CosCos, CosSin }

public class LightTrailer : MonoBehaviour
{
	[Header ("Components:")]
	public Transform nodeObject;

	public Mode mode;

	[Header ("Nodes:")]
	[Range (1, 128)]
	public int nodes = 1;

	[Header ("Speed:")]
	public float xSpeed = 1f;
	public float ySpeed = 1f;

	[Header ("Size")]
	public float xSize = 1f;
	public float ySize = 1f;

	[Header ("Spacing:")]
	[Range (0.01f, 2)]
	public float offset = 0.01f;

	private void Update ()
	{
		if (transform.childCount != nodes)
		{
			if (transform.childCount > nodes)
			{
				int diff = transform.childCount - nodes;

				for (int i = 0; i < diff; i++)
				{
					Destroy (transform.GetChild (1).gameObject);
				}
			}
			else if (nodes > transform.childCount)
			{
				int diff = nodes - transform.childCount;

				for (int i = 0; i < diff; i++)
				{
					Instantiate (nodeObject, new Vector3 (0, 0, 0), Quaternion.identity, transform);
				}
			}
		}

		for (int c = 0; c < transform.childCount; c++)
		{
			Transform currChild = transform.GetChild (c);

			switch (mode)
			{
				case Mode.SinSin:
					currChild.position = new Vector3 (
						Mathf.Sin (Time.time * xSpeed + (c * offset)) * xSize,
						Mathf.Sin (Time.time * ySpeed + (c * offset)) * ySize,
						currChild.position.z);
					break;

				case Mode.SinCos:
					currChild.position = new Vector3 (
						Mathf.Sin (Time.time * xSpeed + (c * offset)) * xSize,
						Mathf.Cos (Time.time * ySpeed + (c * offset)) * ySize,
						currChild.position.z);
					break;

				case Mode.CosCos:
					currChild.position = new Vector3 (
						Mathf.Cos (Time.time * xSpeed + (c * offset)) * xSize,
						Mathf.Cos (Time.time * ySpeed + (c * offset)) * ySize,
						currChild.position.z);
					break;

				case Mode.CosSin:
					currChild.position = new Vector3 (
						Mathf.Cos (Time.time * xSpeed + (c * offset)) * xSize,
						Mathf.Sin (Time.time * ySpeed + (c * offset)) * ySize,
						currChild.position.z);
					break;
			}
		}

		//transform.position += new Vector3 (Mathf.Cos (Time.time) * 0.25f, Mathf.Cos (Time.time * 2f) * 0.25f, 0);

		//transform.Rotate (new Vector3 (0, 0, 360 / 2 * Mathf.Cos (Time.time)) * Time.deltaTime);
		//transform.localScale += new Vector3 (Mathf.Sin (Time.time) * 0.25f, Mathf.Sin (Time.time) * 0.25f, 0);
	}
}
