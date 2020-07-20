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
	public FadeMode_Sprite fadeMode;

	// Variables

	public bool startFade;

	public float speed = 2;

	// Enumerators

	private IEnumerator doFade;

	private void Update ()
	{
		if (startFade)
		{
			startFade = false;

			Fade (fadeMode);
		}
	}

	#region Colour _____________________________________________________________

	private void Set_Image_Alpha (Image img, float alpha)
	{
		img.color = new Color (img.color.r, img.color.g, img.color.b, alpha);
	}

	#endregion

	private void Fade (FadeMode_Sprite mode)
	{
		if (doFade != null) StopCoroutine (doFade);

		if (mode == FadeMode_Sprite.In)
		{
			doFade = Do_Fade_In ();
			StartCoroutine (doFade);

			return;
		}

		if (mode == FadeMode_Sprite.Out)
		{
			doFade = Do_Fade_Out ();
			StartCoroutine (doFade);

			return;
		}
	}

	private IEnumerator Do_Fade_In ()
	{
		while (image.color.a < 1)
		{
			image.color += new Color (0, 0, 0, speed * Time.deltaTime);

			yield return null;
		}

		Set_Image_Alpha (image, 1);
	}

	private IEnumerator Do_Fade_Out ()
	{
		while (image.color.a > 0)
		{
			image.color -= new Color (0, 0, 0, speed * Time.deltaTime);

			yield return null;
		}

		Set_Image_Alpha (image, 0);
	}
}
