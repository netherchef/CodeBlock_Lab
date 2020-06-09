// Scaler v0.02

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScalerType { Increase, Decrease }

public class Scaler : MonoBehaviour
{
	// Components

	public Transform target;

	// Variables

	public float newTargetScale;
	public bool setTargetScale;

	public float speed = 1;
	public bool scaling;
	public Vector3 targetScale;

	public ScalerType scalerType;

	private float initialScale = 1;

	private void Update ()
	{
		if (!scaling)
		{
			if (setTargetScale)
			{
				setTargetScale = false;

				targetScale = new Vector3 (1, 1, 1) * newTargetScale;

				if (target.localScale.x < targetScale.x || target.localScale.y < targetScale.y || target.localScale.z < targetScale.z)
				{
					scalerType = ScalerType.Increase;
				}
				else
				{
					scalerType = ScalerType.Decrease;
				}

				newTargetScale = initialScale;

				scaling = true;
			}

			return;
		}

		switch (scalerType)
		{
			case ScalerType.Increase:

				if (target.localScale.x < targetScale.x || target.localScale.y < targetScale.y || target.localScale.z < targetScale.z)
				{
					target.localScale = Scale_Increase (target.localScale, speed);
				}
				else
				{
					scaling = false;
				}
				break;

			case ScalerType.Decrease:

				if (target.localScale.x > targetScale.x || target.localScale.y > targetScale.y || target.localScale.z > targetScale.z)
				{
					target.localScale = Scale_Decrease (target.localScale, speed);
				}
				else
				{
					scaling = false;
				}
				break;
		}
	}

	#region Increase & Decrease ________________________________________________

	private Vector3 Scale_Increase (Vector3 currScale, float speed)
	{
		return currScale + new Vector3 (speed, speed, speed) * Time.deltaTime;
	}

	private Vector3 Scale_Decrease (Vector3 currScale, float speed)
	{
		return currScale - new Vector3 (speed, speed, speed) * Time.deltaTime;
	}

	#endregion
}
