// Mover v0.02

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoverType { Player, NPC }
public enum MoveState { Stop, Left, Right }

public class Mover : MonoBehaviour
{
	// Components

	public Transform subject;

	// Variables

	public MoverType moverType;
	public MoveState moveState;
	public float speed = 1;

	// Enumerators

	private IEnumerator playerMover;
	private IEnumerator nPCMover;

	private void Start ()
	{
		switch (moverType)
		{
			case MoverType.Player:
				playerMover = PlayerMover ();
				StartCoroutine (playerMover);
				break;

			case MoverType.NPC:
				nPCMover = NPCMover ();
				StartCoroutine (nPCMover);
				break;
		}
	}

	#region Mover Types ________________________________________________________

	private IEnumerator PlayerMover ()
	{
		while (enabled)
		{
			if (Input.GetAxisRaw ("Horizontal") != 0)
			{
				if (Input.GetAxisRaw ("Horizontal") < 0)
					Move_Left ();
				else if (Input.GetAxisRaw ("Horizontal") > 0)
					Move_Right ();
			}
			else
			{
				if (Get_MoveState () != MoveState.Stop) Stop ();

				yield return null;
			}

			Move ();

			yield return null;
		}
	}

	private IEnumerator NPCMover ()
	{
		while (enabled)
		{
			Move ();

			yield return null;
		}
	}

	#endregion

	#region Move _______________________________________________________________

	private void Move ()
	{
		switch (moveState)
		{
			case MoveState.Stop:
				return;

			case MoveState.Left:
				subject.Translate (new Vector2 (-1, 0) * speed * Time.deltaTime);
				break;

			case MoveState.Right:
				subject.Translate (new Vector2 (1, 0) * speed * Time.deltaTime);
				break;
		}
	}

	#endregion

	#region Move States ________________________________________________________

	public void Stop ()
	{
		moveState = MoveState.Stop;
	}

	public void Move_Left ()
	{
		moveState = MoveState.Left;
	}

	public void Move_Right ()
	{
		moveState = MoveState.Right;
	}

	private MoveState Get_MoveState ()
	{
		return moveState;
	}

	#endregion
}
