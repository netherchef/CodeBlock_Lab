using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchModulator : MonoBehaviour
{
	[Header ("Components:")]

	public AudioSource audioSource;

	[Header ("Random Clips:")]

	public AudioClip[] audioClips;

	[Header ("Phonetic Clips:")]

	public AudioClip a;
	public AudioClip e;
	public AudioClip i;
	public AudioClip o;
	public AudioClip u;

	[Header ("Variables:")]

	public float minPitch = 0.75f;
	public float maxPitch = 1.25f;

	public float targetPitch = 1;
	public float speed = 8f;

	[Header ("Debug:")]

	public bool debug;

	private void Update ()
	{
		if (!debug) return;

		if (Input.GetButtonDown ("Submit"))
		{
			audioSource.PlayOneShot (ChooseClip ());

			targetPitch = Random.Range (minPitch, maxPitch);
		}

		// Pitch

		if (Mathf.Abs (audioSource.pitch - targetPitch) > 0.05f)
		{
			float dir = Mathf.Sign (-(audioSource.pitch - targetPitch));

			audioSource.pitch = Mathf.Clamp (audioSource.pitch + dir * speed * Time.deltaTime, minPitch, maxPitch);
		}
	}

	private AudioClip ChooseClip ()
	{
		int val = Random.Range (0, audioClips.Length);
		return audioClips[val];
	}

	#region Phonetics __________________________________________________________

	public void Play_A ()
	{
		targetPitch = Random.Range (minPitch, maxPitch);
		audioSource.PlayOneShot (a);
	}

	public void Play_E ()
	{
		targetPitch = Random.Range (minPitch, maxPitch);
		audioSource.PlayOneShot (e);
	}

	public void Play_I ()
	{
		targetPitch = Random.Range (minPitch, maxPitch);
		audioSource.PlayOneShot (i);
	}

	public void Play_O ()
	{
		targetPitch = Random.Range (minPitch, maxPitch);
		audioSource.PlayOneShot (o);
	}

	public void Play_U ()
	{
		targetPitch = Random.Range (minPitch, maxPitch);
		audioSource.PlayOneShot (u);
	}

	#endregion
}
