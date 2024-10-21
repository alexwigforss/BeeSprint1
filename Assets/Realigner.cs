using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Realigner : MonoBehaviour
{
	Rigidbody rb;
	[SerializeField] float rotateSpeed;
	[SerializeField] float maxRotateSpeed;
	Vector3 rotateForce = Vector3.zero;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		if (rb != null)
		{
			// Apply movement
			rb.transform.Rotate(rotateForce * Time.deltaTime);
			//Vector3 worldMoveForce = rb.transform.TransformDirection(moveForce);
			//rb.velocity += worldMoveForce * Time.deltaTime;
			//CalculateMovement();
		}

	}
	public void AlignAll()
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * 1.0f);
	}

	public void AlignX(bool startstop)
	{
		float xAngle = Mathf.LerpAngle(transform.eulerAngles.x, 0, Time.deltaTime * 1.0f);
		transform.eulerAngles = new Vector3(xAngle, transform.eulerAngles.y, transform.eulerAngles.z );
	}
	public void AlignZ(bool startstop)
	{
		float zAngle = Mathf.LerpAngle(transform.eulerAngles.z, 0, Time.deltaTime * 1.0f);
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zAngle);
	}
	public void AlignXZ(bool startstop)
	{
		Debug.Log("Aligning");
		float xAngle = Mathf.LerpAngle(transform.eulerAngles.x, 0, Time.deltaTime * 2.0f);
		float zAngle = Mathf.LerpAngle(transform.eulerAngles.z, 0, Time.deltaTime * 2.0f);
		transform.eulerAngles = new Vector3(xAngle, transform.eulerAngles.y, zAngle);
	}
	public bool IsXzAligned()
	{
		// Check if the rotation is close to (0, 0, 0)[^1^][1]
		if (Mathf.Abs(transform.eulerAngles.x) < 0.1f && Mathf.Abs(transform.eulerAngles.z) < 0.01f)
		{
			return true;
		}
		return false;
	}
}

/*		Overcompicated realignemt from gpt
		if (startstop)
		{
			Debug.Log("Rotation " + rb.transform.rotation);
			// Gradually reduce the rotation forces to zero
			if (rb.transform.rotation.x > 0.0f)
			{
				if (rotateForce.x < maxRotateSpeed)
				{
					rotateForce.x -= rotateSpeed;
				}
			}
			else if (rb.transform.rotation.x < 0.0f)
			{
				if (rotateForce.x < maxRotateSpeed)
				{
					rotateForce.x += rotateSpeed;
				}
			}
			if (rb.transform.rotation.y > 0.0f)
			{
				if (rotateForce.y < maxRotateSpeed)
				{
					rotateForce.y -= rotateSpeed;
				}
			}
			else if (rb.transform.rotation.y < 0.0f)
			{
				if (rotateForce.y < maxRotateSpeed)
				{
					rotateForce.y += rotateSpeed;
				}
			}
			if (rb.transform.rotation.z > 0.0f)
			{
				if (rotateForce.z < maxRotateSpeed)
				{
					rotateForce.z -= rotateSpeed;
				}
			}
			else if (rb.transform.rotation.z < 0.0f)
			{
				if (rotateForce.z < maxRotateSpeed)
				{
					rotateForce.z += rotateSpeed;
				}
			}
			// Clamp the values to zero to avoid overshooting
			if (Mathf.Abs(rb.rotation.x) < 0.1f)
			{
				rotateForce.x = 0;
				rb.transform.rotation = Quaternion.Euler(0, rb.transform.rotation.eulerAngles.y, rb.transform.rotation.eulerAngles.z);
			}
			if (Mathf.Abs(rb.rotation.y) < 0.1f)
			{
				rotateForce.y = 0;
				rb.transform.rotation = Quaternion.Euler(rb.transform.rotation.eulerAngles.x, 0, rb.transform.rotation.eulerAngles.z);
			}
			if (Mathf.Abs(rb.rotation.z) < 0.1f)
			{
				rotateForce.z = 0;
				rb.transform.rotation = Quaternion.Euler(rb.transform.rotation.eulerAngles.x, rb.transform.rotation.eulerAngles.y, 0);
			}
		}
		else
		{
			// Stop the realignment process
			rotateForce.x = 0.0f;
		}
*/

