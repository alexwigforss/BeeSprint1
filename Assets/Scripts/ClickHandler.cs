using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// The ClickHandler class is responsible for handling mouse clicks in the game.
/// </summary>
public class ClickHandler : MonoBehaviour {
	// private int selectedFlowerSpecie = -1;
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

	/// <summary>
	/// Coroutines that waits for the end of the frame before calling LeftMouseClicked.
	/// </summary>
	/// <remarks>
	/// Waiting for the end of the frame helps ensure that all necessary updates and events are processed,
	/// leading to more reliable and accurate click detection.
	/// </remarks>
	private IEnumerator DeferredLeftMouseClicked() {
		yield return new WaitForEndOfFrame();
		LeftMouseClicked();
	}
	private IEnumerator DeferredRightMouseClicked() {
		yield return new WaitForEndOfFrame();
		RightMouseClicked();
	}

	private void LeftMouseClicked() => DetectClickedObject();
	private void RightMouseClicked() => DetectClickedObject();

	private void DetectClickedObject() {
		// Check if the pointer is over a UI element if not return early.
		if (EventSystem.current.IsPointerOverGameObject()) {
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		if (Physics.Raycast(ray, out RaycastHit hit)) {
			GameObject clickedObject = hit.collider.gameObject;
			// Clickable zones in scene
			if (clickedObject.layer == (int)Layer.Clickable) {
				// Access the parent GameObject
				Transform parentTransform = clickedObject.transform.parent;
				if (parentTransform != null) {
					// Get the Growth component from the parent
					if (parentTransform.TryGetComponent<Growth>(out var parentGrowth)) {
						// Access the id variable from the Growth component
						int parentId = parentGrowth.spawnById;
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

	/// <summary>
	/// Highlights the flowers base of the clicked group.
	/// </summary>
	/// <param name="id"></param>
	private void HighlightFlowerBase(int id) {
		Transform grandchild = GetGrandchildWithId(flowerSpawners.transform, id);
		if (grandchild != null) {
			Debug.Log("Found grandchild with ID " + id + ": " + grandchild.name);
			if (grandchild.TryGetComponent<HighlightFlower>(out var highlightFlower)) {
				highlightFlower.HighlightSelected();
			} else {
				Debug.Log("HighlightFlower component not found on grandchild.");
			}
		} else {
			Debug.Log("Grandchild with ID " + id + " not found.");
		}
	}

	/// <summary>
	/// Gets the flower specie (grandchild) with the specified ID.
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="id"></param>
	/// <returns></returns>
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
