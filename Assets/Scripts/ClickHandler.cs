using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
	private InputAction leftMouseClick , rightMouseClick;

	private void Awake()
	{
		leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
		leftMouseClick.performed += ctx => LeftMouseClicked();
		leftMouseClick.Enable();

		rightMouseClick = new InputAction(binding: "<Mouse>/rightButton");
		rightMouseClick.performed += ctx => RightMouseClicked();
		rightMouseClick.Enable();
	}

	private void LeftMouseClicked()
	{
		DetectClickedObject();
		// print("LeftMouseClicked");
	}

	private void RightMouseClicked()
	{
		DetectClickedObject();
		// print("RightMouseClicked");
	}

	private void DetectClickedObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			GameObject clickedObject = hit.collider.gameObject;
			Debug.Log("Clicked on: " + clickedObject.name);
		}
	}
}
