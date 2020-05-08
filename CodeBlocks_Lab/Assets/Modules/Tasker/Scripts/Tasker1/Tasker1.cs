// Tasker v0.016

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasker1 : MonoBehaviour
{
	// Components

	public Tasker1_JSONeer jsoneer;

	public List<Task1> activeTasks;

	private TaskLibrary taskLibrary;

	// Variables

	public string targetTask;

	public bool beginTask;
	public bool checkReqs;

	private void Start ()
	{
		Load_TaskLibrary ();
	}

	private void Update ()
	{
		if (beginTask)
		{
			beginTask = false;

			Begin (targetTask);
		}

		if (checkReqs)
		{
			checkReqs = false;

			if (Reqs_Fulfilled (targetTask))
			{
				Remove_Task (targetTask);
			}
		}
	}

	private void Load_TaskLibrary ()
	{
		// Pull the task library through JSONeer,
		// and assign it to local task library

		taskLibrary = jsoneer.TaskLibrary_From_JSON ();
	}

	//private bool Task_Visited ()
	//{
	//	// Check if the task has been done before,
	//	// through the save file

	//	return false;
	//}

	private void Begin (string taskName)
	{
		if (taskName == "")
		{
			Debug.LogWarning ("No target task name given.");

			return;
		}
		else if (taskName == Find_ActiveTask (taskName).name)
		{
			Debug.LogWarning ("Task already active.");

			return;
		}

		// Look for a matching task in the task library.
		// If one is found, add a new blank task to the active list under the same name.

		if (Get_Task_From_Library (taskName).name == taskName)
		{
			Task1 blankTask = new Task1 ()
			{
				name = taskName
			};

			Add_To_ActiveTasks (blankTask);
		}
	}

	private void Add_To_ActiveTasks (Task1 task)
	{
		// Add task to Active Task List

		activeTasks.Add (task);
	}

	private bool Reqs_Fulfilled (string taskName)
	{
		// Are the active task's variables identical to its library equivalent?

		Task1 activeTask = Find_ActiveTask (taskName);
		Task1 libraryTask = Get_Task_From_Library (taskName);

		return Equals (activeTask, libraryTask);
	}

	private Task1 Find_ActiveTask (string taskName)
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

	private Task1 Get_Task_From_Library (string taskName)
	{
		// Look through active tasks and return the task with a matching name

		for (int t = 0; t < taskLibrary.tasks.Count; t++)
		{
			if (taskLibrary.tasks[t].name == taskName)
			{
				return taskLibrary.tasks[t];
			}
		}

		// If no matching task is found, log warning

		Debug.LogWarning ("Task not found in library list.");

		return default;
	}

	private void Remove_Task (string taskName)
	{
		// Look through active tasks and remove the task with a matching name

		for (int t = 0; t < activeTasks.Count; t++)
		{
			if (activeTasks[t].name == taskName)
			{
				activeTasks.Remove (activeTasks[t]);

				return;
			}
		}

		// If no matching task is found, log warning

		Debug.LogWarning ("Task not found in active list.");
	}

	//private void End ()
	//{
	//	// Add task name to save file
	//}
}
