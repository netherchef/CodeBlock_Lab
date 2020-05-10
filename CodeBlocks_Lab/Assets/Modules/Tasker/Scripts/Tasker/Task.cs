using System.Collections.Generic;

[System.Serializable]
public struct TaskLibrary
{
	public List<Task> tasks;
}

[System.Serializable]
public struct Task
{
	public string name;
	public bool a, b, c, d, e;
}