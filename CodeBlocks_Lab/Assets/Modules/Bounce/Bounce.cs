using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Bounce : MonoBehaviour
{
	//public float a;
	//public float b;
	//public float result;

	public float duration = 0.25f;
	public float amplitude = 4f;

	private Vector3 startPos;

	private void Start ()
	{
		startPos = transform.position;
	}

	private void Update ()
	{
		float displacement = Mathf.Cos (Time.time) % 1;
		print (Mathf.Cos (Time.time) + " | " + displacement);
		transform.position = startPos + new Vector3 (0, displacement);

		//transform.position = startPos + new Vector3 (0, -sample (Time.time));

		 //result = a % b;
		// result capped at b.

		 //result = a % b / b;
		// result capped at 1.
	}

	float sample (float timeValue)
	{
		timeValue = timeValue % duration / duration;

		print (timeValue + " % " + duration + " = " + timeValue % duration);
		//print (timeValue + " % " + duration + " / " + duration + " = " + timeValue % duration/duration);

		if (timeValue < 0.2f)
		{
			return timeValue / 0.2f * amplitude;
		}
		return (1.0f - (timeValue - 0.2f) / 0.8f) * amplitude;
	}
}
