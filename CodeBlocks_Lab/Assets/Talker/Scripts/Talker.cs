using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talker : MonoBehaviour
{
	// Components

	public Text textDisplay;
	public Conversation conversation;

	// Variables

	public bool talk;
	public bool proceed;

	public string forkSymbol;

	// Enumerators

	private IEnumerator converse;

	private void Start ()
	{
		converse = Converse ();
		StartCoroutine (converse);
	}

	private IEnumerator Converse ()
	{
		while (enabled)
		{
			if (talk)
			{
				for (int i = 0; i < conversation.lines.Length; i++)
				{
					// Check for option

					if (conversation.lines[i].Contains (forkSymbol))
					{
						// Get the fork ID by removing every character starting from 3rd index
						// This allows 3-digit fork IDs
						// Eg: "001 <fork>" => "001"

						string forkID = conversation.lines[i].Remove (3);

						textDisplay.text = forkID;
					}
					else
					{
						// Print line

						textDisplay.text = conversation.lines[i];
					}

					// Wait to proceed

					while (!proceed) yield return null;

					proceed = false;
				}

				textDisplay.text = "";

				talk = false;
			}

			yield return null;
		}
	}
}
