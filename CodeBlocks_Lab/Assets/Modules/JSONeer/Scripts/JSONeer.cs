using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public struct Data_Shell
{
	public List<Data_Core> dataCores_STRICT;
}

[System.Serializable]
public struct Data_Core
{
	public string data_STRICT;
}

public class JSONeer : MonoBehaviour
{
	// Components

	public Data_Shell dataShell;

	// Variables

	public string file = "DataList.json";

	private void OnEnable ()
	{
		string path = Application.persistentDataPath + "/" + file;

		string jsonContent = File.ReadAllText (path);

		dataShell = JsonUtility.FromJson<Data_Shell> (jsonContent);
	}
}
