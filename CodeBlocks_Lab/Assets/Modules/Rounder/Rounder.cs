using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rounder : MonoBehaviour
{
	public float Round_Four (float number)
	{
		float val = Mathf.Abs (number);
		float sign = Mathf.Sign (number);
		float floor = Mathf.Floor (val);

		float diff = val - floor;

		if (diff < Division (4)) return sign * floor;
		else if (diff > Division (4) && diff < Division (4) * 3) return sign * (floor + Division (4) * 2f);
		else if (diff > Division (4) * 3f) return sign * Mathf.Ceil (val);

		Debug.LogError ("Could not Round to Four: " + number + " | " + diff);
		return number;
	}

	public float Round_Eight (float number)
	{
		float val = Mathf.Abs (number);
		float sign = Mathf.Sign (number);
		float floor = Mathf.Floor (val);

		float diff = val - floor;

		if (diff < Division (8)) return sign * floor;
		else if (diff > Division (8) && diff < Division (8) * 3f) return sign * (floor + Division (8) * 2f);
		else if (diff > Division (8) * 3f && diff < Division (8) * 5f) return sign * (floor + Division (8) * 4f);
		else if (diff > Division (8) * 5f && diff < Division (8) * 7f) return sign * (floor + Division (8) * 6f);
		else if (diff > Division (8) * 7f) return sign * Mathf.Ceil (val);

		Debug.LogError ("Could not Round to Eight: " + number + " | " + diff);
		return number;
	}

	private float Division (int divVal)
	{
		return 1 / divVal;
	}
}
