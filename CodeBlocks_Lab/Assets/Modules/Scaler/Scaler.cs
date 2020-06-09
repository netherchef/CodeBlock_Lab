// Scaler v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScalerType { Increase, Decrease }

public class Scaler : MonoBehaviour
{
	// Components

	public Transform target;

	// Variables

	public Vector3 targetScale;
	public bool setTargetScale;

	public float speed = 1;

	public bool scaling;

	public bool changeScale;

	public ScalerType scalerType;

	private void Update ()
	{
		if (changeScale)
		{
			switch (scalerType)
			{
				// Increase

				case ScalerType.Increase:
					target.localScale = Scale_Increase (target.localScale, speed);
					break;

				// Decrease

				case ScalerType.Decrease:

					if (target.localScale.x <= 0 || target.localScale.y <= 0 || target.localScale.z <= 0)
					{
						print ("Breach!");

						print (target.localScale);

						target.localScale = new Vector3 (0, 0, 0);

						changeScale = false;

						return;
					}

					target.localScale = Scale_Decrease (target.localScale, speed);
					break;
			}
		}
	}

	private void Set_Target_Scale (Vector3 targetScale)
	{

	}

	#region Increase & Decrease ________________________________________________

	private Vector3 Scale_Increase (Vector3 currScale, float changeRate)
	{
		return currScale + new Vector3 (changeRate, changeRate, changeRate) * Time.deltaTime;
	}

	private Vector3 Scale_Decrease (Vector3 currScale, float changeRate)
	{
		return currScale - new Vector3 (changeRate, changeRate, changeRate) * Time.deltaTime;
	}

	#endregion
}
