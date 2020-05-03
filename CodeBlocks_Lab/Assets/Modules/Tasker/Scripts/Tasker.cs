using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasker : MonoBehaviour
{
	 //Components
	
	[Header ("Check:")]
	public Task currentTask;
	public bool checking;

	[Header ("Create:")]
	public Task newTask;
	public bool createTask;

	private void Update ()
	{
		if (currentTask.active)
		{
			// Check Task

			checking = true;

			if (currentTask.requireA || currentTask.requireB || currentTask.requireC)
			{
				return;
			}
			else
			{
				currentTask.active = false;
				currentTask.complete = true;

				checking = false;
			}
		}
		else if (createTask)
		{
			// Create Task

			createTask = false;

			currentTask = newTask;
		}


	}
}