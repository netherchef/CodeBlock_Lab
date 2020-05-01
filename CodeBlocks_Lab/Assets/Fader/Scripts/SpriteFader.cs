using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFader : MonoBehaviour
{
	// Components

	public SpriteRenderer SR;

	// Variables

	public bool active;

	public bool fadeOut;
	public bool fadeIn;

	public float speed = 1;

	private void Update ()
	{
		if (active)
		{
			if (fadeOut)
			{
				SR.color -= new Color (0, 0, 0, speed * Time.deltaTime);

				return;
			}
			else if (fadeIn)
			{
				SR.color += new Color (0, 0, 0, speed * Time.deltaTime);

				return;
			}
		}
	}
}
