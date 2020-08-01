using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnMan : MonoBehaviour
{
	// Components

	public Yarn.Unity.DialogueRunner dialogueRunner;
	public Yarn.Unity.DialogueUI dialogueUI;

	public YarnProgram yarnProgram;

	// Variables

	public bool start;
	public bool proceed;

	private void Start ()
	{
		// Add dialogue

		dialogueRunner.Add (yarnProgram);
	}

	private void Update ()
	{
		if (start)
		{
			start = false;

			// If dialogue is NOT running, start dialogue

			if (!dialogueRunner.IsDialogueRunning)
			{
				dialogueRunner.StartDialogue ();
			}
		}

		// Proceed to next dialogue line

		if (dialogueRunner.IsDialogueRunning)
		{
			if (proceed)
			{
				proceed = false;

				dialogueUI.MarkLineComplete ();
			}
		}
	}
}