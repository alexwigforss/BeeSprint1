using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEventHandler : MonoBehaviour, IPointerClickHandler
{
	public GameObject flowerSpawners;

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



	public void OnPointerClick(PointerEventData eventData)
	{
		GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
		Debug.Log("UI Element Clicked: " + clickedObject.name);

		Image clickedImage = clickedObject.GetComponent<Image>();
		if (clickedImage != null)
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
	public void dummyOnPointerClick(PointerEventData eventData)
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


