using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talker : MonoBehaviour
{
	// Components

	public Text textDisplay;

	public Conversation[] conversations;

	// Variables

	public string currSpeaker;

	private void Start ()
	{
		StartCoroutine ("Do_Talk", conversations[0]);
	}

	private IEnumerator Do_Talk (Conversation c)
	{
		// Assign First Speaker

		currSpeaker = c.lines[0].speaker;

		foreach (Line l in c.lines)
		{
			// Switch Speakers

			if (l.speaker != currSpeaker && l.speaker != "")
			{
				currSpeaker = l.speaker;
			}

			// Print Line

			textDisplay.text = l.line;

			// Proceed

			if (Input.GetButtonDown ("Submit")) yield return null;
			while (!Input.GetButtonDown ("Submit")) yield return null;
		}

		// Reset

		textDisplay.text = "";

		currSpeaker = "";
	}
}