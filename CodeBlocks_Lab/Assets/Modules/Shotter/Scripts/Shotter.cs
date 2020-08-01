// Shotter v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayMode { Single, Repeat }

public class Shotter : MonoBehaviour
{
	// Components

	private SpriteRenderer spriteRenderer;

	// Variables

	public PlayMode playMode;

	public Sprite[] sprites;
	public float interval = 0.5f;
	private int currentSpriteIndex;
	private float lastUpdateTime;

	private void Start ()
	{
		spriteRenderer = gameObject.AddComponent<SpriteRenderer> ();
	}

	private void Update ()
	{
		// Single

		if (playMode == PlayMode.Single)
		{
			if (currentSpriteIndex < sprites.Length)
			{
				if (Time.time > lastUpdateTime + interval)
				{
					spriteRenderer.sprite = sprites[currentSpriteIndex];

					currentSpriteIndex++;

					lastUpdateTime = Time.time;
				}
			}
			else
			{
				if (Time.time > lastUpdateTime + interval)
				{
					Destroy (this.gameObject);
				}
			}

			return;
		}

		// Repeat

		if (playMode == PlayMode.Repeat)
		{
			if (currentSpriteIndex < sprites.Length)
			{
				if (Time.time > lastUpdateTime + interval)
				{
					spriteRenderer.sprite = sprites[currentSpriteIndex];

					currentSpriteIndex++;

					lastUpdateTime = Time.time;
				}

				return;
			}

			if (Time.time > lastUpdateTime + interval)
			{
				currentSpriteIndex = 0;

				spriteRenderer.sprite = sprites[0];

				lastUpdateTime = Time.time;
			}
		}
	}
}
