using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Line
{
	public string speaker;
	public string line;
}

[CreateAssetMenu (fileName = "Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
	public Line[] lines;
}