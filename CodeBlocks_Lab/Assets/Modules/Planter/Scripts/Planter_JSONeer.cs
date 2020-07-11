// JSONeer v0.017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public struct Mode_JSON
{
	public string name;
	public List<Type_JSON> types;
}

[System.Serializable]
public struct Type_JSON
{
	public string name;
	public float xPos;
	public float yPos;
}

public class Planter_JSONeer : MonoBehaviour
{
	// Components

	public Planter planter;
	public List<Mode_JSON> modes;

	// Variables

	public string fileName = "Plants.json";
	public bool pullContainer;
	public bool pullPersistent;
	public bool writePersistent;

	private void Update ()
	{
		// Pull from Planter

		if (pullContainer)
		{
			pullContainer = false;

			modes = Modes_From_Planter ();

			return;
		}

		// Read

		if (pullPersistent)
		{
			pullPersistent = false;

			modes = DataContainer_From_JSON ();

			return;
		}

		// Write

		if (writePersistent)
		{
			writePersistent = false;

			DataContainer_To_JSON ();
		}
	}

	private List<Mode_JSON> Modes_From_Planter ()
	{
		// Main Mode Container

		List<Mode_JSON> modesFromPlanter = new List<Mode_JSON> ();

		// Make a copy of the main container holding all the goodies

		GameObject container = planter.container;

		// The container holds multiple Parents.
		// These parent transforms hold the planted objects.

		for (int m = 0; m < container.transform.childCount; m++)
		{
			// For each Parent:
			// Create a new Mode, and name it after the current Parent.
			// Create a new Type list.

			Mode_JSON currMode = new Mode_JSON
			{
				name = container.transform.GetChild (m).name
			};

			List<Type_JSON> newType = new List<Type_JSON> ();

			for (int c = 0; c < container.transform.GetChild (m).childCount; c++)
			{
				// For each of the planted objects:
				// Create a new Type, and grab its values from the current planted object.
				// Add the new Type to the Type list.

				Transform currPlantedObject = container.transform.GetChild (m).GetChild (c);

				Type_JSON currType = new Type_JSON
				{
					name = currPlantedObject.name,
					xPos = currPlantedObject.position.x,
					yPos = currPlantedObject.position.y
				};

				newType.Add (currType);
			}

			// Add the Type list to the new Mode, and add the new Mode to the Main Mode Container.

			currMode.types = newType;

			modesFromPlanter.Add (currMode);
		}

		return modesFromPlanter;
	}

	#region From JSON __________________________________________________________

	public List<Mode_JSON> DataContainer_From_JSON ()
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

				return JsonUtility.FromJson<List<Mode_JSON>> (jsonContent);
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

	private void DataContainer_To_JSON ()
	{
		// Define JSON file path

		string filePath = Application.persistentDataPath + "/" + fileName;

		try
		{
			// Format the struct as a JSON string

			string jsonContent = JsonUtility.ToJson (modes);

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
