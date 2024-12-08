using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEventHandler : MonoBehaviour, IPointerClickHandler
{
	public GameObject flowerSpawners;
	public int selectedBeeGroup = -1 ;
	[SerializeField] GameObject drones;
	private List<int> selectedBeeGroups = new List<int>();
	// LPanel lp = LayoutGroup.AddComponent<LPanel>();
	private LPanel leftPanelRef;

	private void HighlightFlowerBase(Transform spawner)
	{
		if (spawner != null)
		{
			Debug.Log("Found grandchild with ID: " + spawner.name);

			HighlightFlower highlightFlower = spawner.GetComponent<HighlightFlower>();
			if (highlightFlower != null)
			{
				highlightFlower.HighlightSelected();
			}
			else
			{
				Debug.Log("HighlightFlower component not found on grandchild.");
			}
		}
		else
		{
			Debug.Log("Spawner with ID not found.");
		}
	}

		void Start()
		{
			// Get the grandchild Transform
			Transform grandchildTransform = transform.GetChild(1).GetChild(0);
			if (grandchildTransform != null)
			{
			// Get the script component from the grandchild
			leftPanelRef = grandchildTransform.GetComponent<LPanel>();
				if (leftPanelRef != null)
				{
					Debug.Log("LPanel component found on grandchild.");
				// You can now use grandchildScript to call methods or access variables
				leftPanelRef.HelloLPanel();
				}
				else
				{
					Debug.Log("LPanel component not found on grandchild.");
				}
			}
			else
			{
				Debug.Log("Grandchild not found.");
			}
		}


	public void OnPointerClick(PointerEventData eventData)
	{
		GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
		Debug.Log("UI Element Clicked: " + clickedObject.name);

		if (clickedObject.TryGetComponent<Image>(out var clickedImage))
		{
			Sprite clickedSprite = clickedImage.sprite;
			Debug.Log("Clicked Sprite: " + clickedSprite.name + ", " + clickedSprite.texture.ToString());

			// Find the child element with the matching texture
			Transform matchingChild = FindChildWithMatchingTexture(flowerSpawners.transform, clickedSprite.texture);
			if (matchingChild != null)
			{
				Debug.Log("Found matching child: " + matchingChild.name);
				HighlightFlowerBase(matchingChild);
			}
			else
			{
				Debug.Log("No matching child found.");
			}
		}
		else
		{
			Debug.Log("No Image component found on the clicked object.");

			// Get the parent of the clicked object
			Transform parentTransform = clickedObject.transform.parent;
			if (parentTransform != null)
			{
				// Get the grandparent of the clicked object
				Transform grandparentTransform = parentTransform.parent;
				if (grandparentTransform != null)
				{
					// Get the index of the parent under the grandparent
					int parentIndex = parentTransform.GetSiblingIndex();
					int prioSelected = selectedBeeGroup;
					if (selectedBeeGroup < 0)
					{
						Debug.Log("none before");
						selectedBeeGroup = parentIndex;
						EnableSpheres(parentIndex);
						leftPanelRef.SetSpriteSelected(parentIndex);
					}
					else if (selectedBeeGroup == parentIndex)
					{
						Debug.Log("same as before");
						leftPanelRef.UnSetSpriteSelected(selectedBeeGroup);
						DisableSpheres(selectedBeeGroup);
						selectedBeeGroup = -1;
						// TODO Set icon to NOTselected
						// Disable highlight on bees
					}
					else if (selectedBeeGroup != parentIndex)
					{
						selectedBeeGroup = parentIndex;
						leftPanelRef.UnSetSpriteSelected(prioSelected);
						DisableSpheres(prioSelected);
						EnableSpheres(parentIndex);
						leftPanelRef.SetSpriteSelected(selectedBeeGroup);
					}
					else
					{
						Debug.Log("This shloud not happen");
					}
					Debug.Log("Parent's index under the grandparent: " + selectedBeeGroup);
				}
				else
				{
					Debug.Log("Grandparent not found.");
				}
			}
			else
			{
				Debug.Log("Parent not found.");
			}
		}
	}
	int store = -1;
	private void EnableSpheres(int parentIndex)
	{
		Transform group = drones.transform.GetChild(parentIndex);
		foreach (Transform drone in group)
		{
			BeeSelection bs = drone.GetComponent<BeeSelection>();
			if (bs != null)
			{
				bs.EnableSphere();
			}
		}
	}
	
	private void DisableSpheres(int parentIndex)
	{
		Transform group = drones.transform.GetChild(parentIndex);
		foreach (Transform drone in group)
		{
			BeeSelection bs = drone.GetComponent<BeeSelection>();
			if (bs != null)
			{
				bs.DisableSphere();
			}
		}
	}

	private Transform FindChildWithMatchingTexture(Transform parent, Texture2D texture)
	{
		foreach (Transform child in parent)
		{
			Transform flowerSpawner = child.Find("FlowerSpawner");
			if (flowerSpawner != null)
			{
				if (flowerSpawner.TryGetComponent<flowerSpawner>(out var spawnerScript))
				{
					Texture2D childTexture = spawnerScript.texture as Texture2D;
					if (childTexture != null && childTexture == texture)
					{
						return flowerSpawner;
					}
				}
			}
		}
		return null;
	}
	public void DummyOnPointerClick(PointerEventData eventData)
	{
		GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
		Debug.Log("UI Element Clicked: " + clickedObject.name);

		Image clickedImage = clickedObject.GetComponent<Image>();
		if (clickedImage != null)
		{
			// None related code
		}
		else
		{
			Debug.Log("No Image component found on the clicked object.");

			// Get the parent of the clicked object
			Transform parentTransform = clickedObject.transform.parent;
			if (parentTransform != null)
			{
				// Get the grandparent of the clicked object
				Transform grandparentTransform = parentTransform.parent;
				if (grandparentTransform != null)
				{
					// Get the index of the parent under the grandparent
					int parentIndex = parentTransform.GetSiblingIndex();
					Debug.Log("Parent's index under the grandparent: " + parentIndex);
				}
				else
				{
					Debug.Log("Grandparent not found.");
				}
			}
			else
			{
				Debug.Log("Parent not found.");
			}
		}
	}
}


