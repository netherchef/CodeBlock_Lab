using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intervaler : MonoBehaviour
{
	// Variables

	private float timer;
	private bool active;

	private void Update ()
	{
		// If the Submit Button is pressed, activate the timer.

		if (Input.GetButtonDown ("Submit"))
		{
			active = true;
		}

		/// While the timer is active:
		/// 1. If the Submit button is released withing the allotted time, it's a Light Tap!
		/// 2. If it's held longer than the allotted time, it's a Normal Press!

		while (active)
		{
			timer += Time.deltaTime;

			if (Input.GetButtonUp ("Submit"))
			{
				if (timer <= 0.1f)
				{
					print ("Light Tap!");
				}

				timer = 0;

				active = false;

				return;
			}

			if (timer >= 0.1f)
			{
				print ("Normal Press!");

				timer = 0;

				active = false;

				return;
			}

			return;
		}
	}
}
