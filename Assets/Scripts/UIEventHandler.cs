using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEventHandler : MonoBehaviour, IPointerClickHandler {
	public GameObject flowerSpawners;
	public int selectedBeeGroup = -1;
	public GameObject drones;
	public GameObject bee;
	public GameObject bGroupPrefab;
	[SerializeField] Transform nestLocation;
	private LPanel leftPanelRef;

	private void HighlightFlowerBase(Transform spawner) {
		if (spawner != null) {
			HighlightFlower highlightFlower = spawner.GetComponent<HighlightFlower>();
			if (highlightFlower != null) {
				if (highlightFlower.HighlightSelected()) {
					if (selectedBeeGroup >= 0) {
						FlowerSpawner fs = spawner.GetComponent<FlowerSpawner>();
						GetBeeMemory(selectedBeeGroup).AddSpecie(fs.ID);
					}
				} else {
					if (selectedBeeGroup >= 0) {
						FlowerSpawner fs = spawner.GetComponent<FlowerSpawner>();
						GetBeeMemory(selectedBeeGroup).RemoveSpecie(fs.ID);
					}
				}
			} else {
				Debug.Log("HighlightFlower component not found on grandchild.");
			}
		} else {
			Debug.Log("Spawner with ID not found.");
		}
	}

	ISelectiveMemory GetBeeMemory(int index) {
		Transform trans = drones.transform.GetChild(index);
		GameObject go = trans.gameObject;

		if (go.TryGetComponent<ISelectiveMemory>(out var memory)) {
			return memory;
		} else {
			Debug.Log("SelectiveMemory not found");
			return null;
		}
	}

	void Start() {
		Transform grandchildTransform = transform.GetChild(1).GetChild(0);
		if (grandchildTransform != null) {
			leftPanelRef = grandchildTransform.GetComponent<LPanel>();
			if (leftPanelRef != null) {
				Debug.Log("LPanel component found on grandchild.");
			} else {
				Debug.Log("LPanel component not found on grandchild.");
			}
		} else {
			Debug.Log("Grandchild not found.");
		}
	}

	void HighlightSprite(Image image) {
		Outline outline = image.GetComponent<Outline>();
		if (outline != null) {
			UnHighlightSprite(image);
		} else {
			outline = image.gameObject.AddComponent<Outline>();
			outline.effectColor = Color.black;
			outline.effectDistance = new Vector2(5, 5);
		}
	}

	void UnHighlightSprite(Image image) {
		Outline outline = image.GetComponent<Outline>();
		if (outline != null) {
			Destroy(outline);
			Debug.Log("Outline removed from " + image.name);
		} else {
			Debug.LogWarning("No Outline component found on " + image.name);
		}
	}

	public void UnHighlightAllImages() {
		GameObject rightPanel = GameObject.Find("RightPanel");
		if (rightPanel == null) {
			Debug.LogWarning("RightPanel not found.");
			return;
		}

		Transform layoutGroup = rightPanel.transform.Find("LayoutGroup");
		if (layoutGroup == null) {
			Debug.LogWarning("LayoutGroup not found.");
			return;
		}

		foreach (Transform child in layoutGroup) {
			Image image = child.GetComponent<Image>();
			if (image != null) {
				Outline outline = image.GetComponent<Outline>();
				if (outline != null) {
					Destroy(outline);
					Debug.Log("Outline removed from " + image.name);
				}
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData) {
		GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
		Debug.Log("UI Element Clicked: " + clickedObject.name);

		if (clickedObject.TryGetComponent<Image>(out var clickedImage)) {
			HandleImageClick(clickedImage);
		} else if (clickedObject.GetComponent<TextMeshProUGUI>()) {
			HandleTextClick(clickedObject);
		}
	}

	private void HandleImageClick(Image clickedImage) {
		if (clickedImage.tag == "Icon") {
			Debug.Log("Icon CLICKED");
			SpawnBee();
			EnableSpheres(selectedBeeGroup);
			leftPanelRef.SetSpriteSelected(selectedBeeGroup);
		} else if (selectedBeeGroup >= 0) {
			Sprite clickedSprite = clickedImage.sprite;
			Debug.Log("Clicked Sprite: " + clickedSprite.name + ", " + clickedSprite.texture.ToString());
			Transform matchingChild = FindChildWithMatchingTexture(flowerSpawners.transform, clickedSprite.texture);
			if (matchingChild != null) {
				Debug.Log("Found matching child: " + matchingChild.name);
				HighlightFlowerBase(matchingChild);
				HighlightSprite(clickedImage);
			} else {
				Debug.Log("No matching child found.");
			}
		}
	}

	private void HandleTextClick(GameObject clickedObject) {
		Debug.Log("BAM Bee tmp clicked");
		GetSelectedBeeByTMPChild(clickedObject);
		UnHighlightAllImages();
	}

	void SpawnBee() {
		Vector3 spawnPosition = nestLocation.position;
		GameObject spawnedObject = Instantiate(bee, spawnPosition, Quaternion.identity);
		int selectedSprite = selectedBeeGroup;
		if (selectedBeeGroup >= 0 && selectedBeeGroup < drones.transform.childCount) {
			Transform groupTransform = drones.transform.GetChild(selectedBeeGroup);
			spawnedObject.transform.SetParent(groupTransform);
			spawnedObject.GetComponent<Beehave>().state = 3;
		} else {
			GameObject newGroup = Instantiate(bGroupPrefab, drones.transform);
			newGroup.name = "BGroup (" + (drones.transform.childCount - 1) + ")";
			newGroup.transform.SetParent(drones.transform);
			spawnedObject.transform.SetParent(newGroup.transform);
			spawnedObject.GetComponent<Beehave>().state = 1;
			selectedBeeGroup = selectedSprite = drones.transform.childCount - 1;
		}
		if (spawnedObject != null) {
			spawnedObject.GetComponent<Beehave>().HiveLocation = nestLocation;
			spawnedObject.GetComponent<AutoMove>().HiveLocation = nestLocation;
		}
		leftPanelRef.ReGetSprites();
		Debug.Log("selectedSprite is: " + selectedSprite + "selectedBeeGroup is: " + selectedBeeGroup);
		leftPanelRef.SetNewSpriteSelected(selectedSprite);
	}

	private void ShowSelectSpecies(int parentIndex) {
		foreach (Transform spawner in flowerSpawners.transform) {
			foreach (Transform flower in spawner) {
				HighlightFlower hlf = flower.GetComponent<HighlightFlower>();
				FlowerSpawner fs = flower.GetComponent<FlowerSpawner>();
				if (hlf != null) {
					if (GetBeeMemory(selectedBeeGroup).ContainsSpecie(fs.ID)) {
						hlf.HighlightSelected();
					}
				} else {
					Debug.LogWarning("MyScript component not found on " + flower.name);
				}
			}
		}
	}

	private void UnselectAllSpecies() {
		foreach (Transform spawner in flowerSpawners.transform) {
			foreach (Transform flower in spawner) {
				HighlightFlower hlf = flower.GetComponent<HighlightFlower>();
				if (hlf != null) {
					if (hlf.selected == true) {
						hlf.HighlightSelected();
					}
				} else {
					Debug.LogWarning("MyScript component not found on " + flower.name);
				}
			}
		}
	}

	private void EnableSpheres(int parentIndex) {
		Transform group = drones.transform.GetChild(parentIndex);
		foreach (Transform drone in group) {
			BeeSelection bs = drone.GetComponent<BeeSelection>();
			if (bs != null) {
				bs.EnableSphere();
			}
		}
		Debug.Log("Spheres enabled at index: " + parentIndex);
	}

	private void DisableSpheres(int parentIndex) {
		Transform group = drones.transform.GetChild(parentIndex);
		foreach (Transform drone in group) {
			BeeSelection bs = drone.GetComponent<BeeSelection>();
			if (bs != null) {
				bs.DisableSphere();
			}
		}
	}

	private Transform FindChildWithMatchingTexture(Transform parent, Texture2D texture) {
		foreach (Transform child in parent) {
			Transform flowerSpawner = child.Find("FlowerSpawner");
			if (flowerSpawner != null) {
				if (flowerSpawner.TryGetComponent<FlowerSpawner>(out var spawnerScript)) {
					Texture2D childTexture = spawnerScript.texture as Texture2D;
					if (childTexture != null && childTexture == texture) {
						return flowerSpawner;
					}
				}
			}
		}
		return null;
	}

	private void GetSelectedBeeByTMPChild(GameObject clickedObject) {
		Transform parentTransform = clickedObject.transform.parent;
		if (parentTransform != null) {
			Transform grandparentTransform = parentTransform.parent;
			if (grandparentTransform != null) {
				int parentIndex = parentTransform.GetSiblingIndex();
				int prioSelected = selectedBeeGroup;
				if (selectedBeeGroup < 0) {
					selectedBeeGroup = parentIndex;
					EnableSpheres(parentIndex);
					ShowSelectSpecies(parentIndex);
					leftPanelRef.SetSpriteSelected(parentIndex);
				} else if (selectedBeeGroup == parentIndex) {
					leftPanelRef.UnSetSpriteSelected(selectedBeeGroup);
					DisableSpheres(selectedBeeGroup);
					UnselectAllSpecies();
					selectedBeeGroup = -1;
				} else if (selectedBeeGroup != parentIndex) {
					selectedBeeGroup = parentIndex;
					leftPanelRef.UnSetSpriteSelected(prioSelected);
					DisableSpheres(prioSelected);
					UnselectAllSpecies();
					ShowSelectSpecies(parentIndex);
					EnableSpheres(parentIndex);
					leftPanelRef.SetSpriteSelected(selectedBeeGroup);
				} else {
					Debug.Log("This should not happen");
				}
				Debug.Log("Parent's index under the grandparent: " + selectedBeeGroup);
			} else {
				Debug.Log("Grandparent not found.");
			}
		} else {
			Debug.Log("Parent not found.");
		}
	}
}


