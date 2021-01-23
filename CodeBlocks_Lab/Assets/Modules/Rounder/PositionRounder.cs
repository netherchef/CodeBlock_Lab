using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoundMode { NULL, Four, Eight }

[ExecuteInEditMode]
public class PositionRounder : MonoBehaviour
{
	[Header ("Components:")]

	public Transform targetParent;

	[Header ("Scripts:")]

	public Rounder rounder;

	[Header ("Variables:")]

	public RoundMode mode;

	private void OnEnable ()
	{
		if (!targetParent)
		{
			Debug.LogError ("Target Parent NOT Set.");

			this.enabled = false;

			return;
		}

		switch (mode)
		{
			case RoundMode.NULL:
				Debug.LogWarning ("Rounding Mode NOT Set.");
				break;
			case RoundMode.Four:
				Round_To_Four ();
				break;
			case RoundMode.Eight:
				Round_To_Eight ();
				break;
		}

		targetParent = null;

		this.enabled = false;
	}

	private void Round_To_Four ()
	{
		foreach (Transform t in targetParent)
		{
			t.position = new Vector3 (
				rounder.Round_Four (t.position.x), 
				rounder.Round_Four (t.position.y), 
				t.position.z);
		}
	}

	private void Round_To_Eight ()
	{
		foreach (Transform t in targetParent)
		{
			t.position = new Vector3 (
				rounder.Round_Eight (t.position.x),
				rounder.Round_Eight (t.position.y),
				t.position.z);
		}
	}
}
