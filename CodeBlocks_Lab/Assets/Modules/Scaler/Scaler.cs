// Scaler v0.03

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScalerType { NULL, Increase, Decrease }

public class Scaler : MonoBehaviour
{
	// Components

	[Header ("Components:")]

	public Transform target;

	// Variables

	[Header ("Variables:")]

	public float speed = 1;

	[SerializeField]
	private float targetScale;

	[SerializeField]
	private ScalerType scalerType;

	// Enumerators

	private IEnumerator do_Scale;

	private void OnEnable ()
	{
		do_Scale = Do_Scale ();
		StartCoroutine (do_Scale);
	}

	private IEnumerator Do_Scale (float targScale = 0)
	{
		while (enabled)
		{
			// Wait for Scaler Type

			while (scalerType == ScalerType.NULL) yield return null;

			bool Up = scalerType == ScalerType.Increase;
			bool Down = scalerType == ScalerType.Decrease;

			// Set Target Scale

			if (Up && Mathf.Abs (targScale) <= 0.0f)
			{
				targetScale = 1;
			}
			else if (Down && Mathf.Abs (targScale) <= 0.0f)
			{
				targetScale = 0;
			}
			else
			{
				targetScale = targScale;
			}

			while (Up || Down)
			{
				// Check if Should Scale

				Vector3 currScale = transform.localScale;

				if (Up)
				{
					if (currScale.x >= targetScale && currScale.y >= targetScale) Up = false;
				}
				else if (Down)
				{
					if (currScale.x <= targetScale && currScale.y <= targetScale) Down = false;
				}

				// If Should Scale

				if (Up) transform.localScale = Scale_Increase (transform.localScale, speed);
				else if (Down) transform.localScale = Scale_Decrease (transform.localScale, speed);

				// If NOT

				if (!Up && !Down) transform.localScale = new Vector3 (targetScale, targetScale, transform.localScale.z);

				yield return null;
			}

			// Reset

			scalerType = ScalerType.NULL;
		}
	}

	private IEnumerator Scale_Up (float targScale = 0)
	{
		// Set Target Scale

		if (Mathf.Abs (targScale) <= 0.0f) targetScale = 1;
		else targetScale = targScale;

		bool Up = true;

		while (Up)
		{
			// Check if Should Scale

			if (transform.localScale.x >= targetScale && transform.localScale.y >= targetScale) Up = false;

			// If Scaling Done

			if (!Up) transform.localScale = new Vector3 (targetScale, targetScale, transform.localScale.z);

			// If Should Scale

			if (Up) transform.localScale = Scale_Increase (transform.localScale, speed);

			yield return null;
		}
	}

	private IEnumerator Scale_Down (float targScale = 0)
	{
		// Set Target Scale

		if (Mathf.Abs (targScale) <= 0.0f) targetScale = 0;
		else targetScale = targScale;

		bool Down = true;

		while (Down)
		{
			// Check if Should Scale

			if (transform.localScale.x <= targetScale && transform.localScale.y <= targetScale) Down = false;

			// If Scaling Done

			if (!Down) transform.localScale = new Vector3 (targetScale, targetScale, transform.localScale.z);

			// If Should Scale

			if (Down) transform.localScale = Scale_Decrease (transform.localScale, speed);

			yield return null;
		}
	}

	#region Increase & Decrease ________________________________________________

	private Vector3 Scale_Increase (Vector3 currScale, float speed)
	{
		return currScale + new Vector3 (speed, speed, 0) * Time.deltaTime;
	}

	private Vector3 Scale_Decrease (Vector3 currScale, float speed)
	{
		return currScale - new Vector3 (speed, speed, 0) * Time.deltaTime;
	}

	#endregion
}
