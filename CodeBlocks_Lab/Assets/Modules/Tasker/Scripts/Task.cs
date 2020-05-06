using System.Collections.Generic;

[System.Serializable]
public struct Task
{
	public string name;

	public bool active;
	public bool complete;

	public bool requireA, requireB, requireC;
}

#region Task1 __________________________________________________________________

[System.Serializable]
public struct TaskLibrary
{
	public List<Task1> tasks;
}

[System.Serializable]
public struct Task1
{
	public string name;
	public bool a, b, c, d, e;
}

#endregion