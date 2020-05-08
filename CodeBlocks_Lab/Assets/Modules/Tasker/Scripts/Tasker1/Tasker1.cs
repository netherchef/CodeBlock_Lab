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

			if (Are_Reqs_Fulfilled (targetTask))
			{
				Remove_ActiveTask (targetTask);
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

	#region Begin Task _________________________________________________________

	private void Begin (string taskName)
	{
		if (taskName == "")
		{
			Debug.LogWarning ("No target task name given.");

			return;
		}
		else if (Task_Active (taskName))
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

	#endregion

	#region Check Task _________________________________________________________

	private bool Are_Reqs_Fulfilled (string taskName)
	{
		// Are the active task's variables identical to its library equivalent?

		Task1 activeTask = Find_ActiveTask (taskName);
		Task1 libraryTask = Get_Task_From_Library (taskName);

		return Equals (activeTask, libraryTask);
	}

	#endregion

	#region Update Task ________________________________________________________

	public void Fulfill_Task_Req (string taskName, bool a, bool b, bool c, bool d, bool e)
	{
		// Find the task in the active list and make a copy

		Task1 activeTask = Find_ActiveTask (taskName);

		// Compare the input parameters with their equivalent in the copy.
		// If a match is found, log a warning and return.

		if (activeTask.a || activeTask.b || activeTask.c || activeTask.d || activeTask.e)
		{
			Debug.LogWarning (taskName + " | One or more requirements specified already added. Returning...");
		}

		// Update its values, remove the original from the active list, and put the copy back in.

		activeTask.a = a;
		activeTask.b = b;
		activeTask.c = c;
		activeTask.d = d;
		activeTask.e = e;

		Remove_ActiveTask (activeTask.name);

		Add_To_ActiveTasks (activeTask);
	}

	#endregion

	#region Active _____________________________________________________________

	private void Add_To_ActiveTasks (Task1 task)
	{
		// Add task to Active Task List

		activeTasks.Add (task);
	}

	private Task1 Find_ActiveTask (string taskName)
	{
		// Look through active tasks and return the task with a matching name

		for (int t = 0; t < activeTasks.Count; t++)
		{
			if (activeTasks[t].name == taskName) return activeTasks[t];
		}

		// If no matching task is found, log warning

		Debug.LogWarning ("Task not found in active list.");

		return default;
	}

	private bool Task_Active (string taskName)
	{
		for (int t = 0; t < activeTasks.Count; t++)
		{
			if (activeTasks[t].name == taskName) return true;
		}

		return false;
	}

	#endregion

	private Task1 Get_Task_From_Library (string taskName)
	{
		// Look through active tasks and return the task with a matching name

		for (int t = 0; t < taskLibrary.tasks.Count; t++)
		{
			if (taskLibrary.tasks[t].name == taskName) return taskLibrary.tasks[t];
		}

		// If no matching task is found, log warning

		Debug.LogWarning ("Task not found in library list.");

		return default;
	}

	private void Remove_ActiveTask (string taskName)
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
