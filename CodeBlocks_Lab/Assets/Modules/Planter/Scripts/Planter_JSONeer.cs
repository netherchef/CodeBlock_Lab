// JSONeer v0.017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#region Structs ________________________________________________________________

[System.Serializable]
public struct Container
{
	public List<Category> categories;
}

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

#endregion

[ExecuteInEditMode]
public class Planter_JSONeer : MonoBehaviour
{
	// Components

	public Planter planter;
	public Container container;

	// Variables

	[Header ("JSON:")]
	public string fileName = "Plants.json";

	public bool writeToJSON;
	public bool pullFromJSON;

	public string trailingSuffix = "(Clone)(Clone)";

	private void Update ()
	{
		// Write

		if (writeToJSON)
		{
			writeToJSON = false;

			container = Categories_From_Planter ();

			Write_Container_To_JSON ();

			return;
		}

		// Pull

		if (pullFromJSON)
		{
			pullFromJSON = false;

			container = Pull_Container_From_JSON ();

			return;
		}
	}

	private Container Categories_From_Planter ()
	{
		// Main Category List

		List<Category> categoryList = new List<Category> ();

		// Make a copy of the main container holding all the goodies

		Transform mainContainer = planter.holder;

		// The container holds multiple Parents.
		// These Parent transforms hold the Planted Objects.

		for (int m = 0; m < mainContainer.childCount; m++)
		{
			// For each Parent:
			// Create a Category, and name it after the current Parent.
			// Create a new Plant list.

			Category currCategory = new Category
			{
				name = mainContainer.GetChild (m).name
			};

			List<Plant> plantList = new List<Plant> ();

			for (int c = 0; c < mainContainer.GetChild (m).childCount; c++)
			{
				// For each of the Planted Objects:
				// Create a new Plant, and grab its values from the current Planted Object.
				// Add the new Plant to the Plant list.

				Transform currPlantedObject = mainContainer.GetChild (m).GetChild (c);

				int count = currPlantedObject.name.Length - trailingSuffix.Length;

				string correctName = currPlantedObject.name.Remove (count);

				Plant plant = new Plant
				{
					name = correctName,
					position = currPlantedObject.position
			};

				plantList.Add (plant);
			}

			// Add the Plant list to the new Category, and add the new Category to the Main Category List.

			currCategory.plants = plantList;

			categoryList.Add (currCategory);
		}

		// Assign the Category list to that of the Container to be returned.

		return new Container { categories = categoryList };
	}

	#region From JSON __________________________________________________________

	public Container Pull_Container_From_JSON ()
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

				return JsonUtility.FromJson<Container> (jsonContent);
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

	private void Write_Container_To_JSON ()
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
