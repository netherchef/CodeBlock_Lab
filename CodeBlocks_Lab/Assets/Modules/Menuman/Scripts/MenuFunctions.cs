using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFunctions : MonoBehaviour
{
	public delegate void MenuFunction ();

	public List<MenuFunction> functions = new List<MenuFunction> ();

	private void Start ()
	{
		functions.Add (A);
		functions.Add (B);
		functions.Add (C);
		functions.Add (D);
	}

	private void A ()
	{
		print ("A");
	}

	private void B ()
	{
		print ("B");
	}

	private void C ()
	{
		print ("C");
	}

	private void D ()
	{
		print ("D");
	}
}
