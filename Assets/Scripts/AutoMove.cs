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
	float fwdspeed, strspeed, risespeed = 0.0f;
	float maxRotateSpeed = 2.0f;
	float maxMoveSpeed;
	Vector3 moveForce = Vector3.zero;
	Vector3 rotateForce = Vector3.zero;
	Boolean accelerate = false;
	Boolean reverse = false;
	Boolean ascend = false;
	Boolean descend = false;
	Boolean strleft = false;
	Boolean strright = false;
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		maxMoveSpeed = moveSpeed * 20;
		maxRotateSpeed = rotateSpeed * 10;
	}
	private void Update()
	{
		if (rb != null)
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
			Debug.Log(Vector3.Distance(rb.transform.position, HiveLocation.position));
            // MoveRight(true);

            // Apply rotation
            //rb.transform.Rotate(rotateForce);
            rb.transform.Rotate(rotateForce * Time.deltaTime);

			// Apply movement
			Vector3 worldMoveForce = rb.transform.TransformDirection(moveForce);
			rb.velocity += worldMoveForce * Time.deltaTime;

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

			Debug.Log(rotateForce.y);
		}

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

	public void MoveRight(bool startstop)
	{
		if (startstop && rotateForce.y < maxRotateSpeed) { rotateForce.y += rotateSpeed; }
		else if (!startstop) { rotateForce.y = 0; }
	}

	public void MoveLeft(bool startstop)
	{
		if (startstop && rotateForce.y > -maxRotateSpeed) { rotateForce.y -= rotateSpeed; }
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
		if (startstop && rotateForce.x < maxRotateSpeed) { rotateForce.x += rotateSpeed; }
		else if (!startstop) { rotateForce.x = 0; }
	}
	public void RotateLeft(bool startstop)
	{
		if (startstop && rotateForce.z < maxRotateSpeed) { rotateForce.z += rotateSpeed; }
		else if (!startstop) { rotateForce.z = 0; }
	}
	public void PitchDown(bool startstop)
	{
		if (startstop && rotateForce.x > -maxRotateSpeed) { rotateForce.x -= rotateSpeed; }
		else if (!startstop) { rotateForce.x = 0; }
	}
	public void RotateRight(bool startstop)
	{
		if (startstop && rotateForce.z > -maxRotateSpeed) { rotateForce.z -= rotateSpeed; }
		else if (!startstop) { rotateForce.z = 0; }
	}
}

