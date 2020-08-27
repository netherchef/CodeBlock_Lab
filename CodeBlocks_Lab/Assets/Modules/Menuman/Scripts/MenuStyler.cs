using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStyler : MonoBehaviour
{
	#region Select _____________________________________________________________

	public void Select (MenuButton btn)
	{
		Vector3 btnScale = btn.transform.localScale;

		btn.transform.localScale = new Vector3 (
			btnScale.x / 2,
			btnScale.y / 2,
			btnScale.z);
	}

	public void UnSelect (MenuButton btn)
	{
		Vector3 btnScale = btn.transform.localScale;

		btn.transform.localScale = new Vector3 (
			btnScale.x * 2,
			btnScale.y * 2,
			btnScale.z);
	}

	#endregion
}
