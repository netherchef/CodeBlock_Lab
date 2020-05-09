// Tasker v0.017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasker : MonoBehaviour
{
	// Components

	public Tasker_JSONeer jsoneer;

	public List<Task> activeTasks;

	private TaskLibrary taskLibrary;

	// Variables

	public string targetTask;

	public bool beginTask;
	public bool endTask;

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

		if (endTask)
		{
			endTask = false;

			Try_End (targetTask);
		}
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
		// Make sure a proper task name is specified. If not, log a warning.

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
			Task blankTask = new Task ()
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

		Task activeTask = Find_ActiveTask (taskName);
		Task libraryTask = Get_Task_From_Library (taskName);

		return Equals (activeTask, libraryTask);
	}

	#endregion

	#region Update Task ________________________________________________________

	public void Fulfill_Task_Req (string taskName, bool a, bool b, bool c, bool d, bool e)
	{
		// Find the task in the active list and make a copy

		Task copy = Find_ActiveTask (taskName);

		// Compare the input parameters with their equivalent in the copy.
		// If a match is found, log a warning and return.

		if (copy.a || copy.b || copy.c || copy.d || copy.e)
		{
			Debug.LogWarning (taskName + " | One or more requirements specified already added. Returning...");
		}

		// Update its values

		copy.a = a;
		copy.b = b;
		copy.c = c;
		copy.d = d;
		copy.e = e;

		// Remove the original from the active list.
		// If the task has not been fulfilled, add the copy back in.

		Remove_ActiveTask (copy.name);

		if (Are_Reqs_Fulfilled (taskName))
		{
			print ("Task: " + taskName + " | Successfully ended.");

			return;
		}

		Add_To_ActiveTasks (copy);
	}

	#endregion

	#region Active _____________________________________________________________

	private void Add_To_ActiveTasks (Task task)
	{
		// Add task to Active Task List

		activeTasks.Add (task);
	}

	private Task Find_ActiveTask (string taskName)
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

	#endregion

	#region End ________________________________________________________________

	public void Try_End (string taskName)
	{
		// Make sure a proper task name is specified. If not, log a warning.

		if (taskName == "")
		{
			Debug.LogWarning ("No target task name given.");

			return;
		}
		else if (!Task_Active (taskName))
		{
			Debug.LogWarning ("Task is not active active.");

			return;
		}

		// If the task's requirements are fulfilled, remove it from the active list.
		// If not, log a warning.

		if (Are_Reqs_Fulfilled (taskName))
		{
			End_Task (taskName);

			return;
		}

		Debug.LogWarning ("Task: " + taskName + " | End requirements not met.");
	}

	private void End_Task (string taskName)
	{
		Remove_ActiveTask (taskName);
		print ("Task: " + taskName + " | Successfully ended.");
	}

	#endregion

	#region Task Library _______________________________________________________

	private void Load_TaskLibrary ()
	{
		// Pull the task library through JSONeer,
		// and assign it to local task library

		taskLibrary = jsoneer.TaskLibrary_From_JSON ();
	}

	private Task Get_Task_From_Library (string taskName)
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

	#endregion
}
