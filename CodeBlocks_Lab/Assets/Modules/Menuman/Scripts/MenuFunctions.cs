using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFunctions : MonoBehaviour
{
	public delegate void MenuFunction ();

	public List<MenuFunction> functions = new List<MenuFunction> ();

	private void Start ()
	{
		functions.Add (Start_Game);
		functions.Add (Resume_Game);
		functions.Add (Options);
		functions.Add (Quit_Game);
	}

	public void Run_Function (string funcName)
	{
		for (int r = 0; r < functions.Count; r++)
		{
			if (funcName == functions[r].Method.Name)
			{
				functions[r] ();

				return;
			}
		}
	}

	private void Start_Game ()
	{
		print ("Start!");
	}

	private void Resume_Game ()
	{
		print ("Resume!");
	}

	private void Options ()
	{
		print ("Options...");
	}

	private void Quit_Game ()
	{
		print ("Quit game...");

		Application.Quit ();
	}
}
