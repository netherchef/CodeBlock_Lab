using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Song
{
	public string name;
	public string artist;
	public AudioClip clip;
}

public enum WalkmanState { NULL, Playing, Paused, Stopped }

public class Walkman : MonoBehaviour
{
	[Header ("Components:")]

	public AudioSource source;

	[Header ("Variables:")]

	public Song[] songs;

	public WalkmanState state;

	public bool next;

	private int currSongNumber;

	// Enumerators

	private IEnumerator playMusic;

	private void Start ()
	{
		playMusic = PlayMusic ();
		StartCoroutine (playMusic);
	}

	private IEnumerator PlayMusic ()
	{
		source.clip = songs[0].clip;

		source.Play ();

		currSongNumber = 0;

		state = WalkmanState.Playing;

		while (enabled)
		{
			if (next)
			{
				next = false;

				currSongNumber++;

				if (currSongNumber == songs.Length)
				{
					currSongNumber = 0;
				}

				source.clip = songs[currSongNumber].clip;
			}

			if (state == WalkmanState.Playing)
			{
				if (!source.isPlaying) source.Play ();
			}
			else if (state == WalkmanState.Paused)
			{
				if (source.isPlaying) source.Pause ();
			}
			else if (state == WalkmanState.Stopped)
			{
				if (source.isPlaying) source.Stop ();
			}

			yield return null;
		}
	}
}
