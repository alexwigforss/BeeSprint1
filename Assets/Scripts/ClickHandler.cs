using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour {
	int selectedFlowerSpecie = -1;
	public enum Layer { Default = 0, TransparentFX = 1, IgnoreRaycast = 2, Water = 4, UI = 5, Clickable = 6 }
	private InputAction leftMouseClick, rightMouseClick;

	public GameObject flowerSpawners;

	private void Awake() {
		leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
		leftMouseClick.performed += ctx => StartCoroutine(DeferredLeftMouseClicked());
		leftMouseClick.Enable();

		rightMouseClick = new InputAction(binding: "<Mouse>/rightButton");
		rightMouseClick.performed += ctx => StartCoroutine(DeferredRightMouseClicked());
		rightMouseClick.Enable();
	}

	private IEnumerator DeferredLeftMouseClicked() {
		yield return new WaitForEndOfFrame();
		LeftMouseClicked();
	}

	private IEnumerator DeferredRightMouseClicked() {
		yield return new WaitForEndOfFrame();
		RightMouseClicked();
	}

	private void LeftMouseClicked() {
		DetectClickedObject();
	}

	private void RightMouseClicked() {
		DetectClickedObject();
	}

	private void DetectClickedObject() {
		// Check if the pointer is over a UI element
		if (EventSystem.current.IsPointerOverGameObject()) {
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			GameObject clickedObject = hit.collider.gameObject;
			// Clickable zones in scene
			if (clickedObject.layer == (int)Layer.Clickable) {
				// Access the parent GameObject
				Transform parentTransform = clickedObject.transform.parent;
				if (parentTransform != null) {
					// Get the Growth component from the parent
					Growth parentGrowth = parentTransform.GetComponent<Growth>();
					if (parentGrowth != null) {
						// Access the id variable from the Growth component
						int parentId = parentGrowth.spawnById;
						selectedFlowerSpecie = parentGrowth.spawnById;
						Debug.Log("Parent's ID: " + parentId);
						HighlightFlowerBase(parentId);
					} else {
						Debug.Log("Growth component not found on parent.");
					}
				} else {
					Debug.Log("Clicked object has no parent.");
				}
			}
		}
	}

	private void HighlightFlowerBase(int id) {
		Transform grandchild = GetGrandchildWithId(flowerSpawners.transform, id);
		if (grandchild != null) {
			Debug.Log("Found grandchild with ID " + id + ": " + grandchild.name);

			HighlightFlower highlightFlower = grandchild.GetComponent<HighlightFlower>();
			if (highlightFlower != null) {
				highlightFlower.HighlightSelected();
			} else {
				Debug.Log("HighlightFlower component not found on grandchild.");
			}
		} else {
			Debug.Log("Grandchild with ID " + id + " not found.");
		}
	}

	Transform GetGrandchildWithId(Transform parent, int id) {
		foreach (Transform child in parent) {
			foreach (Transform grandchild in child) {
				FlowerSpawner fspwn = grandchild.GetComponent<FlowerSpawner>();
				if (fspwn != null && fspwn.ID == id) {
					return grandchild;
				}
			}
		}
		return null;
	}
}
