using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasker1_Participant : MonoBehaviour
{
	// Components

	public Tasker1 tasker;

	// Variables

	public string taskName;

	private void OnTriggerEnter2D (Collider2D collision)
	{
		tasker.Fulfill_Task_Req (taskName, true, false, false, false, false);
	}
}
