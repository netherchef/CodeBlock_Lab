// JSONeer v0.016

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONeer : MonoBehaviour
{
	// Components

	public DataContainer container;

	// Variables

	public string fileName = "DataList.json";
	public bool check;

	private void Update ()
	{
		if (check)
		{
			check = false;

			container = DataContainer_From_JSON ();
		}
	}

	#region Functions __________________________________________________________

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
}
