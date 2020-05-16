// Tooler v0.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
	A, B
}

public class Tooler : MonoBehaviour
{
	// Components

	public Tool_A toolA;
	public Tool_B tool_B;

	// Variables

	public ToolType toolType;

	public bool useTool;

	private void Update ()
	{
		if (useTool)
		{
			useTool = false;

			Use_Tool ();
		}
	}

	private void Use_Tool ()
	{
		switch (toolType)
		{
			case ToolType.A:
				toolA.Use ();
				break;
			case ToolType.B:
				tool_B.Use ();
				break;
		}
	}
}
