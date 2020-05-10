using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasker_Participant : MonoBehaviour
{
	// Components

	public Tasker tasker;

	// Variables

	public string taskName = "Task1";

	private void OnTriggerEnter2D (Collider2D collision)
	{
		if (tasker.Task_Active (taskName))
		{
			tasker.Fulfill_Task_Req (taskName, true, false, false, false, false);
		}
	}
}
