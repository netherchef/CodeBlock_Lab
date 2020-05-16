// Tooler v0.011

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooler : MonoBehaviour
{
	// Components

	public List<Transform> toolTargets;

	// Variables

	public ToolType equipped;

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
		for (int t = 0; t < toolTargets.Count; t++)
		{
			// Find the first target that requires the tool equipped,
			// and perform an action.

			if (toolTargets[t].GetComponent<ToolTarget> ().require == equipped)
			{
				// Perform action

				toolTargets[t].gameObject.SetActive (false);

				return;
			}
		}
	}
}