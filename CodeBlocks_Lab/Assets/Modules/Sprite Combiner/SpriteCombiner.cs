  using UnityEngine;
  using System.Collections;
using System.Collections.Generic;
public class SpriteCombiner : MonoBehaviour
{
	public SpriteRenderer spriteRenderer;

	private IEnumerator do_Set;

	private void Start ()
	{
		//do_Set = Do_Set ();
		//StartCoroutine (do_Set);

		//for (int x = 0; x < spriteRenderer.sprite.texture.width; x++)
		//{
		//	for (int y = 0; y < spriteRenderer.sprite.texture.height; y++)
		//	{
		//		print (spriteRenderer.sprite.texture.GetPixel (x, y));

		//		if (spriteRenderer.sprite.texture.GetPixel (x, y).a > 0)
		//		{
		//			spriteRenderer.sprite.texture.SetPixel (x, y, Color.red);
		//			spriteRenderer.sprite.texture.Apply ();
		//		}
		//	}
		//}
	}

	private IEnumerator Do_Set ()
	{
		while (enabled)
		{
			for (int x = 0; x < spriteRenderer.sprite.texture.width; x++)
			{
				for (int y = 0; y < spriteRenderer.sprite.texture.height; y++)
				{
					if (spriteRenderer.sprite.texture.GetPixel (x, y).a > 0)
					{
						spriteRenderer.sprite.texture.SetPixel (x, y, new Color (1, 1, 1, .5f));
						spriteRenderer.sprite.texture.Apply ();
					}
				}
			}

			yield return null;
		}
	}
}