// Sounder v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounder : MonoBehaviour
{
	// Components

	public AudioSource audioPlayer;

	// Variables

	public AudioClip sound;

	public bool playC;
	public bool playD;
	public bool playE;
	public bool playF;
	public bool playG;
	public bool playA;
	public bool playB;
	public bool nextOctave;

	private void Update ()
	{
		if (playC)
		{
			playC = false;

			audioPlayer.pitch = 1f;

			audioPlayer.PlayOneShot (sound);
		}
		else if (playD)
		{
			playD = false;

			audioPlayer.pitch = 1 + Eighth ();

			audioPlayer.PlayOneShot (sound);
		}
		else if (playE)
		{
			playE = false;

			audioPlayer.pitch = 1 + Quarter () + Sixteenth () / 4;

			audioPlayer.PlayOneShot (sound);
		}
		else if (playF)
		{
			playF = false;

			audioPlayer.pitch = 1 + Quarter () + Eighth () / 4 + Sixteenth ();

			audioPlayer.PlayOneShot (sound);
		}
		else if (playG)
		{
			playG = false;

			audioPlayer.pitch = 1 + Quarter () * 2;

			audioPlayer.PlayOneShot (sound);
		}
		else if (playA)
		{
			playA = false;

			audioPlayer.pitch = 1 + Quarter () * 2 + Sixteenth () * 3;

			audioPlayer.PlayOneShot (sound);
		}
		else if (playB)
		{
			playB = false;

			audioPlayer.pitch = 1 + Quarter () + Half () + Sixteenth () * 2 + Sixteenth () / 2;

			audioPlayer.PlayOneShot (sound);
		}
		else if (nextOctave)
		{
			nextOctave = false;

			audioPlayer.pitch = 2f;

			audioPlayer.PlayOneShot (sound);
		}
	}

	private float Half ()
	{
		return 1f / 2;
	}

	private float Quarter ()
	{
		return 1f / 4;
	}

	private float Eighth ()
	{
		return 1f / 8;
	}

	private float Sixteenth ()
	{
		return 1f / 16;
	}
}
