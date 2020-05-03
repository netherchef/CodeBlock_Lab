using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class JSON_Data
{
	public string s;
}

public class JSONeer : MonoBehaviour
{
	private void Start ()
	{
		string path = Application.persistentDataPath + "/DataList.json";

		string jsonContent = File.ReadAllText (path);

		JSON_Data d = JsonUtility.FromJson<JSON_Data> (jsonContent);

		print (d.s);
	}
}
