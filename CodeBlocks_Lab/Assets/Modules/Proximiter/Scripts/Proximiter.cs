using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximiter : MonoBehaviour
{
	// Components

	public Transform player;
	public Transform[] npc;

	// Variables

	public float range = 1;
	public bool check;

	private void Update ()
	{
		if (check)
		{
			check = false;

			for (int n = 0; n < npc.Length; n++)
			{
				float gap = (player.position - npc[n].position).magnitude;

				if (gap < range)
				{
					print (gap);
				}
			}
		}
	}
}
