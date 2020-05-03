using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnspinnerIntegrater : MonoBehaviour
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
		dialogueRunner.Add (yarnProgram);
	}

	private void Update ()
	{
		if (start)
		{
			start = false;

			if (!dialogueRunner.IsDialogueRunning)
			{
				dialogueRunner.StartDialogue ();
			}
		}

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