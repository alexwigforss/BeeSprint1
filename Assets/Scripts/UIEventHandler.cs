using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class UIEventHandler : MonoBehaviour, IPointerClickHandler
{
	public GameObject flowerSpawners;
	public int selectedBeeGroup = -1;
	public GameObject drones;
	public GameObject bee;
	public GameObject bGroupPrefab;
	[SerializeField] Transform nestLocation;
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
						//GetBeeMemory(selectedBeeGroup).PrintSpecies();
					}
				}
				else
				{
					if (selectedBeeGroup >= 0)
					{
						FlowerSpawner fs = spawner.GetComponent<FlowerSpawner>();
						GetBeeMemory(selectedBeeGroup).RemoveSpecie(fs.ID);
						//GetBeeMemory(selectedBeeGroup).PrintSpecies();
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
				// leftPanelRef.HelloLPanel();
			}
			else { Debug.Log("LPanel component not found on grandchild."); }
		}
		else { Debug.Log("Grandchild not found."); }
	}

	void HighlightSprite(Image image)
	{
		Outline outline = image.GetComponent<Outline>();
		if (outline != null)
		{
			// If the image is already highlighted, unhighlight it
			UnHighlightSprite(image);
		}
		else
		{
			// If the image is not highlighted, add the outline
			outline = image.gameObject.AddComponent<Outline>();
			outline.effectColor = Color.black;
			outline.effectDistance = new Vector2(5, 5);
		}
	}

	void UnHighlightSprite(Image image)
	{
		Outline outline = image.GetComponent<Outline>();
		if (outline != null)
		{
			Destroy(outline);
			Debug.Log("Outline removed from " + image.name);
		}
		else
		{
			Debug.LogWarning("No Outline component found on " + image.name);
		}
	}

	public void UnHighlightAllImages()
	{
		// Find the RightPanel GameObject
		GameObject rightPanel = GameObject.Find("RightPanel");
		if (rightPanel == null)
		{
			Debug.LogWarning("RightPanel not found.");
			return;
		}

		// Find the LayoutGroup GameObject
		Transform layoutGroup = rightPanel.transform.Find("LayoutGroup");
		if (layoutGroup == null)
		{
			Debug.LogWarning("LayoutGroup not found.");
			return;
		}

		// Iterate through all children of the LayoutGroup
		foreach (Transform child in layoutGroup)
		{
			Image image = child.GetComponent<Image>();
			if (image != null)
			{
				// Check if the image has an Outline component
				Outline outline = image.GetComponent<Outline>();
				if (outline != null)
				{
					// Unhighlight the image by removing the Outline component
					Destroy(outline);
					Debug.Log("Outline removed from " + image.name);
				}
			}
		}
	}


	public void OnPointerClick(PointerEventData eventData)
	{
		GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
		Debug.Log("UI Element Clicked: " + clickedObject.name);

		// Click on image
		if (clickedObject.TryGetComponent<Image>(out var clickedImage))
		{
			if (clickedImage.tag == "Icon")
			{
				Debug.Log("Icon CLICKED");
				SpawnBee(); // If spawnbee created new group
				EnableSpheres(selectedBeeGroup);
				leftPanelRef.SetSpriteSelected(selectedBeeGroup);
			}
			else if (selectedBeeGroup >= 0)
			{
				Sprite clickedSprite = clickedImage.sprite;
				Debug.Log("Clicked Sprite: " + clickedSprite.name + ", " + clickedSprite.texture.ToString());
				Transform matchingChild = FindChildWithMatchingTexture(flowerSpawners.transform, clickedSprite.texture);
				if (matchingChild != null)
				{
					Debug.Log("Found matching child: " + matchingChild.name);
					HighlightFlowerBase(matchingChild);
					HighlightSprite(clickedImage);
				}
				else
				{
					Debug.Log("No matching child found.");
				}
			}
		}

		// Else click was on TMP or panel ()
		else if (clickedObject.GetComponent<TextMeshProUGUI>())
		{
			Debug.Log("BAM Bee tmp clicked");
			GetSelectedBeeByTMPChild(clickedObject);
			UnHighlightAllImages();
		}

			


		void GetSelectedBeeByTMPChild(GameObject clickedObject)
		{
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
					//From none to one selected
					if (selectedBeeGroup < 0)
					{
						// Debug.Log("Bee group was none");
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
					}
					else { Debug.Log("This shloud not happen"); }
					Debug.Log("Parent's index under the grandparent: " + selectedBeeGroup);
				}
				else { Debug.Log("Grandparent not found."); }
			}
			else { Debug.Log("Parent not found."); }
		}
	}

	void SpawnBee()
	{
		Vector3 spawnPosition = nestLocation.position;
		GameObject spawnedObject = Instantiate(bee, spawnPosition, Quaternion.identity);
		int selectedSprite = selectedBeeGroup;
		bool ng = false;
		if (selectedBeeGroup >= 0 && selectedBeeGroup < drones.transform.childCount)
		{
			Transform groupTransform = drones.transform.GetChild(selectedBeeGroup);
			spawnedObject.transform.SetParent(groupTransform);
			spawnedObject.GetComponent<Beehave>().state = 3;
			// spawnedObject.GetComponent<Beehave>().GetTargetFromSibling();
		}
		else
		{
			// Create a new group and attach to it
			GameObject newGroup = Instantiate(bGroupPrefab, drones.transform);
			newGroup.name = "BGroup (" + (drones.transform.childCount - 1) + ")";
			newGroup.transform.SetParent(drones.transform);
			spawnedObject.transform.SetParent(newGroup.transform);
			spawnedObject.GetComponent<Beehave>().state = 1;
			selectedBeeGroup = selectedSprite = drones.transform.childCount - 1;
		}
		if (spawnedObject != null)
		{
			spawnedObject.GetComponent<Beehave>().HiveLocation = nestLocation;
			spawnedObject.GetComponent<AutoMove>().HiveLocation = nestLocation;
		}
		leftPanelRef.ReGetSprites();
		Debug.Log("selectedSprite is: " + selectedSprite + "selectedBeeGroup is: " + selectedBeeGroup);
		leftPanelRef.SetNewSpriteSelected(selectedSprite);
		// spawnedObject.GetComponent<BeeSelection>().EnableSphere();
	}

	private void ShowSelectSpecies(int parentIndex)
	{

		foreach (Transform spawner in flowerSpawners.transform)
		{
			foreach (Transform flower in spawner)
			{
				HighlightFlower hlf = flower.GetComponent<HighlightFlower>();
				FlowerSpawner fs = flower.GetComponent<FlowerSpawner>();
				if (hlf != null)
				{
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
		Debug.Log("Spheres enabled at index: " + parentIndex);
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
}


