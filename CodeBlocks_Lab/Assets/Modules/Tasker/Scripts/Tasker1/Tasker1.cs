// Tasker v0.015

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasker1 : MonoBehaviour
{
	// Components

	public List<Task> activeTasks;

	private Task[] taskLibrary;

	private bool Task_Visited ()
	{
		// Check if the task has been done before,
		// through the save file

		return false;
	}

	private void Begin ()
	{
		Add_To_ActiveTasks (Get_Task_From_Library (""));
	}

	private void Add_To_ActiveTasks (Task task)
	{
		// Add task to Active Task List

		activeTasks.Add (task);
	}

	private bool Are_Reqs_Fulfilled (string taskName)
	{
		Task activeTask = Find_ActiveTask (taskName);
		Task libraryTask = Get_Task_From_Library (taskName);

		return Equals (activeTask, libraryTask);
	}

	private Task Find_ActiveTask (string taskName)
	{
		// Look through active tasks and return the task with a matching name

		for (int t = 0; t < activeTasks.Count; t++)
		{
			if (activeTasks[t].name == taskName)
			{
				return activeTasks[t];
			}
		}

		// If no matching task is found, log warning

		Debug.LogWarning ("Task not found in active list.");

		return default;
	}

	private Task Get_Task_From_Library (string taskName)
	{
		// Look through active tasks and return the task with a matching name

		for (int t = 0; t < taskLibrary.Length; t++)
		{
			if (taskLibrary[t].name == taskName)
			{
				return taskLibrary[t];
			}
		}

		// If no matching task is found, log warning

		Debug.LogWarning ("Task not found in library list.");

		return default;
	}

	//private void End ()
	//{
	//	// Add task name to save file
	//}
}
