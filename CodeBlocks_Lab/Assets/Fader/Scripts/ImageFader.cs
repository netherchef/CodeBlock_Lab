using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
	// Components

	public Image image;

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
				image.color -= new Color (0, 0, 0, speed * Time.deltaTime);

				return;
			}
			else if (fadeIn)
			{
				image.color += new Color (0, 0, 0, speed * Time.deltaTime);

				return;
			}
		}
	}
}
