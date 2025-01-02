using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Realigner : MonoBehaviour {
	Rigidbody rb;
	[SerializeField] float rotateSpeed;
	[SerializeField] float maxRotateSpeed;
	Vector3 rotateForce = Vector3.zero;

	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update() {
		if (rb != null) {
			// Apply movement
			rb.transform.Rotate(rotateForce * Time.deltaTime);
			//Vector3 worldMoveForce = rb.transform.TransformDirection(moveForce);
			//rb.velocity += worldMoveForce * Time.deltaTime;
			//CalculateMovement();
		}

	}
	public void AlignAll() {
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * 1.0f);
	}

	public void AlignX(bool startstop) {
		float xAngle = Mathf.LerpAngle(transform.eulerAngles.x, 0, Time.deltaTime * 1.0f);
		transform.eulerAngles = new Vector3(xAngle, transform.eulerAngles.y, transform.eulerAngles.z);
	}
	public void AlignZ(bool startstop) {
		float zAngle = Mathf.LerpAngle(transform.eulerAngles.z, 0, Time.deltaTime * 1.0f);
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zAngle);
	}
	public bool AlignXZ(bool startstop) {
		float xAngle = Mathf.LerpAngle(transform.eulerAngles.x, 0, Time.deltaTime * 2.0f);
		float zAngle = Mathf.LerpAngle(transform.eulerAngles.z, 0, Time.deltaTime * 2.0f);
		if (xAngle < 0.001 && xAngle > -0.001) {
			if (zAngle < 0.001 && zAngle > -0.001) {
				transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);
				return true;
			}
		}
		// Debug.Log("Aligning " + transform.rotation.x + " " + transform.rotation.z);
		transform.eulerAngles = new Vector3(xAngle, transform.eulerAngles.y, zAngle);
		return false;
	}
	public bool IsXzAligned() {
		// Check if the rotation is close to (0, 0, 0)[^1^][1]
		if (Mathf.Abs(transform.eulerAngles.x) < 0.1f && Mathf.Abs(transform.eulerAngles.z) < 0.01f) {
			return true;
		}
		return false;
	}
}