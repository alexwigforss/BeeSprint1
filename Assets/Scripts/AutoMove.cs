using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class AutoMove : MonoBehaviour
{
	Rigidbody rb;
	public Transform HiveLocation;

	[SerializeField] float acceleration;
	[SerializeField] float moveSpeed;
	[SerializeField] float rotateSpeed;
	[SerializeField] float maxSpeed = 2.0f;
	[SerializeField] float maxRotateSpeed;

	float fwdspeed, strspeed, risespeed = 0.0f;
	float maxMoveSpeed;

	Vector3 moveForce = Vector3.zero;
	Vector3 rotateForce = Vector3.zero;
	bool accelerate = false;
	bool reverse = false;
	bool ascend = false;
	bool descend = false;
	bool strleft = false;
	bool strright = false;
	bool parked = false;
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		// rb.transform.position = HiveLocation.position;
		maxMoveSpeed = moveSpeed * 20;
		maxRotateSpeed = rotateSpeed * 10;
	}
	private void Update()
	{
		if (rb != null)
		{
			// Apply movement
			rb.transform.Rotate(rotateForce * Time.deltaTime);
			Vector3 worldMoveForce = rb.transform.TransformDirection(moveForce);
			rb.velocity += worldMoveForce * Time.deltaTime;
			CalculateMovement();
		}

		void CalculateMovement()
		{
			// Apply movement with Acceleration / Deceleration
			if (accelerate && fwdspeed < maxSpeed) { fwdspeed += acceleration; moveForce.z = fwdspeed; }
			else if (!accelerate && fwdspeed > 0.0f) { fwdspeed -= acceleration; moveForce.z = fwdspeed; }
			if (reverse && fwdspeed > -maxSpeed) { fwdspeed -= acceleration; moveForce.z = fwdspeed; }
			else if (!reverse && fwdspeed < 0.0f) { fwdspeed += acceleration; moveForce.z = fwdspeed; }
			if (fwdspeed > -0.1 && fwdspeed < 0.1) { fwdspeed = moveForce.z = 0.0f; }

			if (ascend && risespeed < maxSpeed / 2) { risespeed += acceleration; moveForce.y = risespeed; }
			else if (!ascend && risespeed > 0.0f) { risespeed -= acceleration; moveForce.y = risespeed; }
			if (descend && risespeed > -maxSpeed / 2) { risespeed -= acceleration; moveForce.y = risespeed; }
			else if (!descend && risespeed < 0.0f) { risespeed += acceleration; moveForce.y = risespeed; }
			if (risespeed > -0.1 && risespeed < 0.1) { risespeed = moveForce.y = 0.0f; }
		}
	}

	public void ResetAll()
	{
		Ascend(false);
		Descend(false);
		MoveRight(false);
		MoveLeft(false);
		StrifeLeft(false);
		StrifeLeft(false);
		MoveForward(false);
		MoveBackward(false);
		PitchUp(false);
		RotateLeft(false);
		PitchDown(false);
		RotateRight(false);
	}

	public void RotateAround()
	{
		StrifeLeft(true);
		Ascend(true);
		rb.transform.LookAt(HiveLocation);
		if (Vector3.Distance(rb.transform.position, HiveLocation.position) > 0.5f)
		{
			MoveForward(true);
		}
		else
		{
			MoveForward(false);
		}
		rb.transform.Rotate(rotateForce * Time.deltaTime);
	}

	public void Ascend(bool startstop)
	{
		if (startstop) { ascend = true; }
		else if (!startstop) { ascend = false; }
	}
	public void Descend(bool startstop)
	{
		if (startstop) { descend = true; }
		else if (!startstop) { descend = false; }
	}
	public void MoveRight(bool startstop, int multi = 1)
	{
		if (startstop && rotateForce.y < maxRotateSpeed) { rotateForce.y += rotateSpeed * multi; }
		else if (!startstop) { rotateForce.y = 0; }
	}
	public void MoveLeft(bool startstop, int multi = 1)
	{
		if (startstop && rotateForce.y > -maxRotateSpeed) { rotateForce.y -= rotateSpeed * multi; }
		else if (!startstop) { rotateForce.y = 0; }
	}
	public void StrifeRight(bool startstop)
	{
		if (startstop) { moveForce.x = maxMoveSpeed * 4; }
		else if (!startstop) { moveForce.x = 0; }
	}
	public void StrifeLeft(bool startstop)
	{
		if (startstop) { moveForce.x = -maxMoveSpeed * 4; }
		else if (!startstop) { moveForce.x = 0; }
	}
	public void MoveForward(bool startstop)
	{
		if (startstop) { accelerate = true; }
		else if (!startstop) { accelerate = false; }
	}
	public void MoveBackward(bool startstop)
	{
		if (startstop) { reverse = true; }
		else if (!startstop) { reverse = false; }
	}
	// NUMPAD
	public void PitchUp(bool startstop)
	{
		if (startstop && rotateForce.x < maxRotateSpeed)
		{
			rotateForce.x += rotateSpeed;
		}
		else if (!startstop) { rotateForce.x = 0; }
	}
	public void RotateLeft(bool startstop)
	{
		if (startstop && rotateForce.z < maxRotateSpeed)
		{
			rotateForce.z += rotateSpeed;
		}
		else if (!startstop)
		{
			rotateForce.z = 0;
		}
	}
	public void PitchDown(bool startstop)
	{
		if (startstop && rotateForce.x > -maxRotateSpeed)
		{
			rotateForce.x -= rotateSpeed;
		}
		else if (!startstop)
		{
			rotateForce.x = 0;
		}
	}
	public void RotateRight(bool startstop)
	{
		if (startstop && rotateForce.z > -maxRotateSpeed)
		{
			rotateForce.z -= rotateSpeed;
		}
		else if (!startstop) { rotateForce.z = 0; }
	}
	public void TurnTowards(Transform goal)
	{
		//Vector3 direction = goal.position - transform.position;
		//// Calculate the rotation needed to look at the target
		//Quaternion targetRotation = Quaternion.LookRotation(direction);
		//// Smoothly rotate towards the target rotation
		//transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
		rb.transform.LookAt(goal);
	}
	public void rotateTowards(Vector3 to, float turn_speed = 2.0f)
	{
		
		Quaternion _lookRotation =
			Quaternion.LookRotation((to - transform.position).normalized);

		transform.rotation =
			Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turn_speed);

		//instant
		//transform.rotation = _lookRotation;
	}

}
