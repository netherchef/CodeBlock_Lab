// JSONeer v0.015

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#region Structs ________________________________________________________________

[System.Serializable]
public struct DataContainer
{
	public List<DataBranch> branchName_STRICT;
}

[System.Serializable]
public struct DataBranch
{
	public string variableName_STRICT;
}

#endregion

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
		DataContainer output = new DataContainer ();

		// Define JSON file path

		string filePath = Application.persistentDataPath + "/" + fileName;

		try
		{
			// Pull data from the JSON file as a string

			string jsonContent = File.ReadAllText (filePath);

			// Convert the string from JSON and load it into a struct

			output = JsonUtility.FromJson<DataContainer> (jsonContent);
		}
		catch
		{
			Debug.LogWarning ("JSON file failed to load.");

			return output;
		}

		return output;
	}

	#endregion
}
