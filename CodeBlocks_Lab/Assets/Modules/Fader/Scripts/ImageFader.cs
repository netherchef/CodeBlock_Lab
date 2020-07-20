// Image Fader v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeMode { In, Out }

public class ImageFader : MonoBehaviour
{
	// Components

	public Image image;
	public FadeMode fadeMode;
	public Text text;

	// Variables

	public bool fade;

	public float speed = 2;

	private void Update ()
	{
		text.text = image.color.a.ToString ();

		if (fade)
		{
			if (fadeMode == FadeMode.Out)
			{
				if (image.color.a > 0)
				{
					image.color -= new Color (0, 0, 0, speed * Time.deltaTime);
				}
				else
				{
					image.color = new Color (image.color.r, image.color.g, image.color.b, 0);
				}

				return;
			}
			else if (fadeMode == FadeMode.In)
			{
				if (image.color.a < 1)
				{
					image.color += new Color (0, 0, 0, speed * Time.deltaTime);
				}
				else
				{
					image.color = new Color (image.color.r, image.color.g, image.color.b, 1);
				}

				return;
			}
		}
	}
}
