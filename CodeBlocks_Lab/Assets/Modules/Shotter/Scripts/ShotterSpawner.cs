using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotterSpawner : MonoBehaviour
{
	// Variables

	public bool spawn;

	public string targetAnimation;
	public Vector3 position;
	public PlayMode playMode;

	public GameObject[] animations;

	private void Update ()
	{
		if (spawn)
		{
			spawn = false;

			Do_Shot (targetAnimation, Vector3.zero, PlayMode.Repeat);
		}
	}

	public void Do_Shot (string targAnim, Vector3 pos, PlayMode mode)
	{
		Set_TargAnim (targAnim);
		Set_Location (pos);
		Set_PlayMode (mode);

		for (int a = 0; a < animations.Length; a++)
		{
			if (animations[a].name == targetAnimation)
			{
				GameObject anim = Instantiate (animations[a], position, Quaternion.identity);

				anim.GetComponent<Shotter> ().playMode = playMode;
			}
		}
	}

	public void Set_TargAnim (string animName)
	{
		targetAnimation = animName;
	}

	public void Set_Location (Vector3 targPos)
	{
		position = targPos;
	}

	public void Set_PlayMode (PlayMode mode)
	{
		playMode = mode;
	}
}