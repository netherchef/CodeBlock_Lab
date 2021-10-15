using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAround : MonoBehaviour
{
	public Transform master;
	public Transform target;

	public bool chase;

	public float moveSpeed = 2f;
	public float turnSpeed = 72f;
	public float range = 2f;

	private Vector3 moveDir = new Vector3 ();

	private bool sideStepping = false;

	private int obstacleLayer;

	private void Start ()
	{
		obstacleLayer = LayerMask.GetMask ("Obstacle");
	}

	private void Update ()
	{
		if (Input.GetButtonDown ("Submit")) chase = !chase;

		if (!chase) return;

		Debug.DrawLine (master.position, master.position + moveDir * range);

		master.position += moveDir * moveSpeed * Time.deltaTime;
	}

	private void FixedUpdate ()
	{
		// Direction to Target

		Vector3 dirToTarg = Vector3.Normalize (target.position - master.position);

		Debug.DrawLine (master.position, master.position + dirToTarg * range, Color.magenta);

		// Check for Obstacle

		RaycastHit2D obstacle = Physics2D.Raycast (master.position, dirToTarg, range, obstacleLayer);
		if (obstacle)
		{
			// If there is an Obstacle between us and the target, 
			// and we're NOT Sidestepping:

			if (!sideStepping)
			{
				sideStepping = true;

				moveDir = dirToTarg;

				// Rotate Vector Until No Obstacle

				int leftScore = 20;
				int rightScore = 20;

				bool checking = true;

				Vector3 checkDirLeft = dirToTarg;

				while (rightScore > 0 && checking)
				{
					rightScore--;

					Debug.DrawLine (master.position, master.position + checkDirLeft * (range * 2f), Color.green);

					checkDirLeft = RotateVector (checkDirLeft, turnSpeed);

					checking = Physics2D.Raycast (master.position, checkDirLeft, range * 4f, obstacleLayer);
				}

				checking = true;

				Vector3 checkDirRight = dirToTarg;

				while (leftScore > 0 && checking)
				{
					leftScore--;

					Debug.DrawLine (master.position, master.position + checkDirRight * (range * 2f), Color.red);

					checkDirRight = RotateVector (checkDirRight, -turnSpeed);

					checking = Physics2D.Raycast (master.position, checkDirRight, range * 4f, obstacleLayer);
				}

				Debug.Break ();

				print ("Left: " + leftScore + " | Right: " + rightScore);

				if (rightScore > leftScore)
				{
					// Go Right

					moveDir = checkDirRight;

					print ("Going Right");
				}
				else
				{
					// Go Left

					moveDir = checkDirLeft;

					print ("Going Left");
				}
			}

			// If there is an Obstacle but we are already Sidestepping,
			// just keep moving in the same direction.
		}
		else
		{
			moveDir = dirToTarg;

			if (sideStepping) sideStepping = false;
		}
	}

	Vector3 RotateVector (Vector3 vector, float degree)
	{
		return Quaternion.Euler (0, 0, degree) * vector;
	}

	// Bunny83
	// https://answers.unity.com/questions/1229302/rotate-a-vector2-around-the-z-axis-on-a-mathematic.html
}
