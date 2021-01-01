using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounder : MonoBehaviour
{
	// Components

	public AudioSource audioSource;

	public AudioClip hover;
	public AudioClip select;

	public void Play_Hover_Sound ()
	{
		audioSource.PlayOneShot (hover);
	}

	public void Play_Select_Sound ()
	{
		audioSource.PlayOneShot (select);
	}
}
