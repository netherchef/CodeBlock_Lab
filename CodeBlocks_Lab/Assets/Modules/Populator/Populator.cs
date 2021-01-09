using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Populator : MonoBehaviour
{
	// Components

	[Header ("Components:")]

	public GameObject targetObject;

	private void Update ()
	{
		if (targetObject)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			float comparisonX = Mathf.Abs (Mathf.Ceil (mousePos.x) - mousePos.x);

			if (comparisonX <= 0.6f && comparisonX >= 0.4f)
			{
				float sign = Mathf.Sign (mousePos.x);
				mousePos.x = sign * (Mathf.Floor (Mathf.Abs (mousePos.x)) + 0.5f);
			}
			else
			{
				mousePos.x = Mathf.Round (mousePos.x);
			}

			float comparisonY = Mathf.Abs (Mathf.Ceil (mousePos.y) - mousePos.y);

			if (comparisonY <= 0.6f && comparisonY >= 0.4f)
			{
				float sign = Mathf.Sign (mousePos.y);
				mousePos.y = sign * (Mathf.Floor (Mathf.Abs (mousePos.y)) + 0.5f);
			}
			else
			{
				mousePos.y = Mathf.Round (mousePos.y);
			}

			targetObject.transform.position = mousePos;
		}

		if (Input.GetMouseButtonDown (0))
		{
			targetObject = null;
		}
	}
}
