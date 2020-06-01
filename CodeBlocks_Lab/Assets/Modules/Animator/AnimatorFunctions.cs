using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
	// Components

	public Animator animator;

	private int animHash_Moving = Animator.StringToHash ("Moving");
	private int animHash_FacingRight = Animator.StringToHash ("FacingRight");

	private bool Is_Moving ()
	{
		return animator.GetBool (animHash_Moving);
	}

	private void Set_Moving (bool state)
	{
		animator.SetBool (animHash_Moving, state);
	}

	private bool Is_FacingRight ()
	{
		return animator.GetBool (animHash_FacingRight);
	}

	private void Set_FacingRight (bool state)
	{
		animator.SetBool (animHash_FacingRight, state);
	}
}
