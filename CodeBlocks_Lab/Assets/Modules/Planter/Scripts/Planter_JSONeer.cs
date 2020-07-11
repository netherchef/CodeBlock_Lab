// JSONeer v0.017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public struct Category
{
	public string name;
	public List<Plant> plants;
}

[System.Serializable]
public struct Plant
{
	public string name;
	public Vector2 position;
}

public class Planter_JSONeer : MonoBehaviour
{
	// Components

	public Planter planter;
	public List<Category> categories;

	// Variables

	public string fileName = "Plants.json";
	public bool pullContainer;
	public bool pullPersistent;
	public bool writePersistent;

	public string trailingSuffix = "(Clone)(Clone)";

	private void Update ()
	{
		// Pull from Planter

		if (pullContainer)
		{
			pullContainer = false;

			categories = Categories_From_Planter ();

			return;
		}

		// Read

		if (pullPersistent)
		{
			pullPersistent = false;

			categories = DataContainer_From_JSON ();

			return;
		}

		// Write

		if (writePersistent)
		{
			writePersistent = false;

			DataContainer_To_JSON ();
		}
	}

	private List<Category> Categories_From_Planter ()
	{
		// Main Category List

		List<Category> categoriesFromPlanter = new List<Category> ();

		// Make a copy of the main container holding all the goodies

		GameObject container = planter.container;

		// The container holds multiple Parents.
		// These Parent transforms hold the Planted Objects.

		for (int m = 0; m < container.transform.childCount; m++)
		{
			// For each Parent:
			// Create a Category, and name it after the current Parent.
			// Create a new Plant list.

			Category currCategory = new Category
			{
				name = container.transform.GetChild (m).name
			};

			List<Plant> plantList = new List<Plant> ();

			for (int c = 0; c < container.transform.GetChild (m).childCount; c++)
			{
				// For each of the Planted Objects:
				// Create a new Plant, and grab its values from the current Planted Object.
				// Add the new Plant to the Plant list.

				Transform currPlantedObject = container.transform.GetChild (m).GetChild (c);

				int count = currPlantedObject.name.Length - trailingSuffix.Length;

				string correctName = currPlantedObject.name.Remove (count);

				Plant plant = new Plant
				{
					name = correctName,
					position = currPlantedObject.position
				};

				plantList.Add (plant);
			}

			// Add the Type list to the new Mode, and add the new Mode to the Main Category List.

			currCategory.plants = plantList;

			categoriesFromPlanter.Add (currCategory);
		}

		return categoriesFromPlanter;
	}

	#region From JSON __________________________________________________________

	public List<Category> DataContainer_From_JSON ()
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

				return JsonUtility.FromJson<List<Category>> (jsonContent);
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

			string jsonContent = JsonUtility.ToJson (categories);

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
