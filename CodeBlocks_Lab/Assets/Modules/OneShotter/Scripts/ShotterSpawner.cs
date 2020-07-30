using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotterSpawner : MonoBehaviour
{
	// Variables

	public bool spawn;

	public string targetAnimation;

	public GameObject[] animations;

	private void Update ()
	{
		if (spawn)
		{
			spawn = false;

			Do_Shot (targetAnimation, Vector3.zero, PlayMode.Single);
		}
	}

	private void Do_Shot (string targAnim, Vector3 pos, PlayMode mode)
	{
		for (int a = 0; a < animations.Length; a++)
		{
			if (animations[a].name == targAnim)
			{
				GameObject anim = Instantiate (animations[a], pos, Quaternion.identity);

				anim.GetComponent<Shotter> ().playMode = mode;
			}
		}
	}
}