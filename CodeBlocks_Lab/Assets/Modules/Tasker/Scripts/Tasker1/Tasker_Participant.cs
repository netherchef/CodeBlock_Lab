using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasker_Participant : MonoBehaviour
{
	// Components

	public Tasker tasker;

	// Variables

	public string taskName;

	private void OnTriggerEnter2D (Collider2D collision)
	{
		tasker.Fulfill_Task_Req (taskName, true, false, false, false, false);
	}
}
