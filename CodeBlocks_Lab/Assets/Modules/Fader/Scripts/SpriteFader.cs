// Sprite Fader v0.02

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FadeMode_Sprite { In, Out }

public class SpriteFader : MonoBehaviour
{
	// Components

	public SpriteRenderer SR;
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

	private void Set_SR_Alpha (SpriteRenderer sr, float alpha)
	{
		sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, alpha);
	}

	#endregion

	private void Fade (FadeMode_Sprite fadeMode)
	{
		if (doFade != null) StopCoroutine (doFade);

		if (fadeMode == FadeMode_Sprite.In)
		{
			doFade = Do_Fade_In ();
			StartCoroutine (doFade);

			return;
		}

		if (fadeMode == FadeMode_Sprite.Out)
		{
			doFade = Do_Fade_Out ();
			StartCoroutine (doFade);

			return;
		}
	}

	private IEnumerator Do_Fade_In ()
	{
		while (SR.color.a < 1)
		{
			SR.color += new Color (0, 0, 0, speed * Time.deltaTime);

			yield return null;
		}

		Set_SR_Alpha (SR, 1);
	}

	private IEnumerator Do_Fade_Out ()
	{
		while (SR.color.a > 0)
		{
			SR.color -= new Color (0, 0, 0, speed * Time.deltaTime);

			yield return null;
		}

		Set_SR_Alpha (SR, 0);
	}
}
