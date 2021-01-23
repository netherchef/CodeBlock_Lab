using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rounder : MonoBehaviour
{
	public float Round_Half (float number)
	{
		float val = Mathf.Abs (number);
		float sign = Mathf.Sign (number);
		float floor = Mathf.Floor (val);

		float diff = val - floor;

		if (diff < 0.25f) return sign * floor;
		else if (diff > 0.25f && diff < 0.75f) return sign * (floor + 0.5f);
		else if (diff > 0.75f) return sign * Mathf.Ceil (val);

		Debug.LogError ("Could not Round to Half: " + number);
		return number;
	}

	public float Round_Fourth (float number)
	{
		float val = Mathf.Abs (number);
		float sign = Mathf.Sign (number);
		float floor = Mathf.Floor (val);

		float diff = val - floor;

		if (diff < 0.125f) return sign * floor;
		else if (diff > 0.125f && diff < 0.375f) return sign * (floor + 0.25f);
		else if (diff > 0.375f && diff < 0.625f) return sign * (floor + 0.5f);
		else if (diff > 0.625f && diff < 0.875f) return sign * (floor + 0.75f);
		else if (diff > 0.875f) return sign * Mathf.Ceil (val);

		Debug.LogError ("Could not Round to Fourth: " + number);
		return number;
	}
}
