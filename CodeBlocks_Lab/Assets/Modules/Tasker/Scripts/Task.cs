[System.Serializable]
public struct Task
{
	public string name;

	public bool active;
	public bool complete;

	public bool requireA, requireB, requireC;
}

public struct Task1
{
	public string name;
	public bool a, b, c, d, e;
}