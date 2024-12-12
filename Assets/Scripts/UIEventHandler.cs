using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class UIEventHandler : MonoBehaviour, IPointerClickHandler
{
	public GameObject flowerSpawners;
	public int selectedBeeGroup = -1;
	public GameObject drones;
	//[SerializeField] GameObject drones;
	private LPanel leftPanelRef;

	private void HighlightFlowerBase(Transform spawner)
	{
		if (spawner != null)
		{
			// Debug.Log("Found grandchild with ID: " + spawner.name);
			HighlightFlower highlightFlower = spawner.GetComponent<HighlightFlower>();
			if (highlightFlower != null)
			{
				if (highlightFlower.HighlightSelected())
				{
					// TODO add id to chossen Bgrup if there is one.
					if (selectedBeeGroup >= 0)
					{
						FlowerSpawner fs = spawner.GetComponent<FlowerSpawner>();
						GetBeeMemory(selectedBeeGroup).AddSpecie(fs.ID);
						GetBeeMemory(selectedBeeGroup).PrintSpecies();
					}
				}
				else
				{
					if (selectedBeeGroup >= 0)
					{
						FlowerSpawner fs = spawner.GetComponent<FlowerSpawner>();
						GetBeeMemory(selectedBeeGroup).RemoveSpecie(fs.ID);
						GetBeeMemory(selectedBeeGroup).PrintSpecies();
					}
					Debug.Log("Specie was unselected");
				}
			}
			else
			{
				// TODO is unselected so remove its id from chossen Bgrup if there is one.


				Debug.Log("HighlightFlower component not found on grandchild.");
			}
		}
		else { Debug.Log("Spawner with ID not found."); }
	}

	SelectiveMemory GetBeeMemory(int index)
	{
		Transform trans = drones.transform.GetChild(index);
		GameObject go = trans.gameObject;

		SelectiveMemory memory = go.GetComponent<SelectiveMemory>();
		if (memory != null)
		{
			return memory;
		}
		else
		{
			Debug.Log("SelectiveMemory not found");
			return null;
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
				leftPanelRef.HelloLPanel();
			}
			else { Debug.Log("LPanel component not found on grandchild."); }
		}
		else { Debug.Log("Grandchild not found."); }
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
		Debug.Log("UI Element Clicked: " + clickedObject.name);

		// Click on image (Flower)
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
				// TODO Get the ID of the spawner
				// TODO Pass it to the begroup if one is selected
			}
			else { Debug.Log("No matching child found."); }
		}
		// Else click was on TMP or panel
		else if (clickedObject.GetComponent<TextMeshProUGUI>())
		{
			Debug.Log("TMP component found on the clicked object.");
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
						// Debug.Log("none before");
						selectedBeeGroup = parentIndex;
						EnableSpheres(parentIndex);
						ShowSelectSpecies(parentIndex);
						leftPanelRef.SetSpriteSelected(parentIndex);
					}
					// None selected
					else if (selectedBeeGroup == parentIndex)
					{
						// Debug.Log("same as before");
						leftPanelRef.UnSetSpriteSelected(selectedBeeGroup);
						DisableSpheres(selectedBeeGroup);
						UnselectAllSpecies();
						selectedBeeGroup = -1;
					}
					// Another selected
					else if (selectedBeeGroup != parentIndex)
					{
						selectedBeeGroup = parentIndex;
						// Unselect previous
						leftPanelRef.UnSetSpriteSelected(prioSelected);
						DisableSpheres(prioSelected);
						UnselectAllSpecies();
						// Select current
						ShowSelectSpecies(parentIndex);
						EnableSpheres(parentIndex);

						leftPanelRef.SetSpriteSelected(selectedBeeGroup);
						// TODO Reselect species from memory
					}
					else { Debug.Log("This shloud not happen"); }
					Debug.Log("Parent's index under the grandparent: " + selectedBeeGroup);
				}
				else { Debug.Log("Grandparent not found."); }
			}
			else { Debug.Log("Parent not found."); }
		}
	}

	private void ShowSelectSpecies(int parentIndex)
	{
		
		foreach (Transform spawner in flowerSpawners.transform)
		{
			foreach (Transform flower in spawner)
			{
				// Get the script component on the child
				HighlightFlower hlf = flower.GetComponent<HighlightFlower>();
				FlowerSpawner fs = flower.GetComponent<FlowerSpawner>();
				if (hlf != null)
				{
					//Debug.Log(fs.ID);
					// Set the variable on the script
					GetBeeMemory(selectedBeeGroup).PrintSpecies();
					if (GetBeeMemory(selectedBeeGroup).ContainsSpecie(fs.ID))
					{
						hlf.HighlightSelected();
					}
				}
				else { Debug.LogWarning("MyScript component not found on " + flower.name); }
			}
		}
	}

	private void UnselectAllSpecies()
	{
		foreach (Transform spawner in flowerSpawners.transform)
		{
			foreach (Transform flower in spawner)
			{
				// Get the script component on the child
				HighlightFlower hlf = flower.GetComponent<HighlightFlower>();
				if (hlf != null)
				{
					// Set the variable on the script
					if (hlf.selected == true)
					{
						hlf.HighlightSelected();
					}
				}
				else { Debug.LogWarning("MyScript component not found on " + flower.name); }
			}
		}
	}

	private void EnableSpheres(int parentIndex)
	{
		Transform group = drones.transform.GetChild(parentIndex);
		foreach (Transform drone in group)
		{
			BeeSelection bs = drone.GetComponent<BeeSelection>();
			if (bs != null) { bs.EnableSphere(); }
		}
	}

	private void DisableSpheres(int parentIndex)
	{
		Transform group = drones.transform.GetChild(parentIndex);
		foreach (Transform drone in group)
		{
			BeeSelection bs = drone.GetComponent<BeeSelection>();
			if (bs != null) { bs.DisableSphere(); }
		}
	}

	private Transform FindChildWithMatchingTexture(Transform parent, Texture2D texture)
	{
		foreach (Transform child in parent)
		{
			Transform flowerSpawner = child.Find("FlowerSpawner");
			if (flowerSpawner != null)
			{
				if (flowerSpawner.TryGetComponent<FlowerSpawner>(out var spawnerScript))
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
	/*
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
	}*/
}


