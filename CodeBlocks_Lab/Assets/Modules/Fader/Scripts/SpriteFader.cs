// Sprite Fader v0.01

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

	public bool fade;

	public float speed = 2;

	private void Update ()
	{
		if (fade)
		{
			if (fadeMode == FadeMode_Sprite.Out)
			{
				if (SR.color.a > 0)
				{
					SR.color -= new Color (0, 0, 0, speed * Time.deltaTime);
				}
				else
				{
					SR.color = new Color (SR.color.r, SR.color.g, SR.color.b, 0);
				}

				return;
			}
			else if (fadeMode == FadeMode_Sprite.In)
			{
				if (SR.color.a < 1)
				{
					SR.color += new Color (0, 0, 0, speed * Time.deltaTime);
				}
				else
				{
					SR.color = new Color (SR.color.r, SR.color.g, SR.color.b, 1);
				}

				return;
			}
		}
	}
}
