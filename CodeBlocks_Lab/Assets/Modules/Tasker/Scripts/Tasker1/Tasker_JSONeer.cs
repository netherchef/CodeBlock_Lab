// JSONeer v0.015

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Tasker_JSONeer : MonoBehaviour
{
	// Components

	public TaskLibrary taskLibrary;

	// Variables

	public string fileName = "TaskLibrary.json";
	public bool check;

	private void Update ()
	{
		if (check)
		{
			check = false;

			taskLibrary = TaskLibrary_From_JSON ();
		}
	}

	#region Functions __________________________________________________________

	public TaskLibrary TaskLibrary_From_JSON ()
	{
		// Define JSON file path

		string filePath = Application.persistentDataPath + "/" + fileName;

		try
		{
			// Pull data from the JSON file as a string

			string jsonContent = File.ReadAllText (filePath);

			try
			{
				// Convert the string from JSON and load it into a struct

				return JsonUtility.FromJson<TaskLibrary> (jsonContent);
			}
			catch
			{
				Debug.LogWarning ("JSON parsing failed.");

				return default;
			}
		}
		catch
		{
			Debug.LogWarning ("JSON file failed to load.");

			return default;
		}
	}

	#endregion
}
