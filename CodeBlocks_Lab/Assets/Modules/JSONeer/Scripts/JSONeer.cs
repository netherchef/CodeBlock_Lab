// JSONeer v0.017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONeer : MonoBehaviour
{
	[Header ("Scripts:")]

	public DataContainer container;

	[Header ("Variables:")]

	public string fileName = "DataList.json";
	public bool check;
	public bool write;

	private void Update ()
	{
		// Read

		if (check)
		{
			check = false;

			container = DataContainer_From_JSON ();

			return;
		}

		// Write

		if (write)
		{
			write = false;

			Write_DataContainer_To_JSON ();
		}
	}

	#region From JSON __________________________________________________________

	public DataContainer DataContainer_From_JSON ()
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

				return JsonUtility.FromJson<DataContainer> (jsonContent);
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

	#region To JSON ____________________________________________________________

	private void Write_DataContainer_To_JSON ()
	{
		// Define JSON file path

		string filePath = Application.persistentDataPath + "/" + fileName;

		try
		{
			// Format the struct as a JSON string

			string jsonContent = JsonUtility.ToJson (container);

			// Write the JSON string to persistent data

			File.WriteAllText (filePath, jsonContent);
		}
		catch
		{
			Debug.LogWarning ("JSON file failed to write.");
		}
	}

	#endregion
}
