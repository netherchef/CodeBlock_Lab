using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkChaser : MonoBehaviour
{
	// Components

	[Header ("Chaser Settings:")]
	public GameObject chaser;
	public float chaserSpeed = 2f;

	[Header ("Target Position Settings:")]
	public GameObject currentTarget;
	public float range = 2f;
	public float updateInterval = 2f;

	[Header ("Target Container Settings:")]
	public GameObject targetContainer;
	public float containerSpeed = 3f;

	[Header ("Rotater Settings:")]
	public GameObject rotater;
	public float rotationSpeed = 180f;

	[Header ("Target Settings:")]
	public GameObject target;

	// Variables

	private Vector3 currTargPos;

	private WaitForSeconds intervalWait;

	// Enumerator

	private IEnumerator update_Curr_Targ_Pos;
	private IEnumerator move_Targ_Container;
	private IEnumerator rotate;
	private IEnumerator move_Chaser;

	private void OnEnable ()
	{
		intervalWait = new WaitForSeconds (updateInterval);

		update_Curr_Targ_Pos = Update_Curr_Targ_Pos ();
		StartCoroutine (update_Curr_Targ_Pos);

		move_Targ_Container = Move_Targ_Container ();
		StartCoroutine (move_Targ_Container);

		rotate = Rotate ();
		StartCoroutine (rotate);

		move_Chaser = Move_Chaser ();
		StartCoroutine (move_Chaser);
	}

	#region Update Current Target Position _____________________________________

	private IEnumerator Update_Curr_Targ_Pos ()
	{
		while (enabled)
		{
			currTargPos = Random_Pos_In_Range ();

			if (currentTarget) currentTarget.transform.position = currTargPos;
			//if (targetContainer) targetContainer.transform.position = currTargPos;

			yield return intervalWait;
		}
	}

	private Vector3 Random_Pos_In_Range ()
	{
		return Random.insideUnitCircle * range;
	}

	#endregion

	#region Move Target Container ______________________________________________

	private IEnumerator Move_Targ_Container ()
	{
		while (enabled)
		{
			//Vector3 moveVal = -(targetContainer.transform.position - currTargPos) * Time.deltaTime * 2;

			//targetContainer.transform.Translate (moveVal);

			while (targetContainer.transform.position == currTargPos) yield return null;

			Vector3 unprocDir = Unprocessed_Direction (targetContainer.transform, currTargPos);

			Vector3 moveVal = -(targetContainer.transform.position - currTargPos) * Time.deltaTime * containerSpeed;

			if (moveVal.magnitude < unprocDir.magnitude)
			{
				//targetContainer.transform.Translate (moveVal);

				Translate_To_Pos (targetContainer.transform, currTargPos, containerSpeed);
			}
			else
			{
				targetContainer.transform.position = currTargPos;
			}

			yield return null;
		}
	}

	#endregion

	#region Rotate _____________________________________________________________

	private IEnumerator Rotate ()
	{
		while (enabled)
		{
			rotater.transform.Rotate (new Vector3 (0, 0, rotationSpeed * Mathf.Cos (Time.time)) * Time.deltaTime);

			yield return null;
		}
	}

	#endregion

	#region Move Chaser ________________________________________________________

	private IEnumerator Move_Chaser ()
	{
		while (enabled)
		{
			if (chaser.transform.position != target.transform.position)
			{
				Translate_To_Pos (chaser.transform, target.transform.position, chaserSpeed);
			}

			yield return null;
		}
	}

	private void Translate_To_Pos (Transform chaser, Vector3 targPos, float speed)
	{
		Vector3 unprocDir = Unprocessed_Direction (chaser, targPos);
		Vector3 dir = Processed_Direction (unprocDir);

		Vector3 moveVal = dir * Time.deltaTime * speed;

		if (moveVal.magnitude < unprocDir.magnitude)
		{
			chaser.transform.Translate (moveVal);

			return;
		}

		chaser.transform.position = targPos;
	}

	private Vector3 Processed_Direction (Vector3 unprocDir)
	{
		float absX = Mathf.Abs (unprocDir.x);
		float absY = Mathf.Abs (unprocDir.y);

		if (absX > absY) unprocDir /= absX;
		else unprocDir /= absY;

		return unprocDir;
	}

	private Vector3 Unprocessed_Direction (Transform chaser, Vector3 targPos)
	{
		return -(chaser.position - targPos);
	}

	#endregion
}
