using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LPanel : Menu
{
	[SerializeField] Texture2D icon;
	[SerializeField] Texture2D selecticon;
	private List<GameObject> spriteObjects = new();

	protected override List<Sprite> GetSprites()
	{
		List<Sprite> sprites = new();
		TextMeshProUGUI tmp = new();
		Texture2D texture = icon;
		if (texture != null)
		{
			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			foreach (Transform item in spawnersParent)
			{
				if (item != null)
				{
					if (item.name.Contains("Group"))
					{
						int noOfBeesInGroup = 0;
						GameObject spriteObject = GetBeeIcon(sprite, item);
						spriteObjects.Add(spriteObject); // Store the reference
						tmp = GetIconText(spriteObject);
						foreach (Transform subitem in item)
						{
							noOfBeesInGroup++;
						}
						tmp.text = noOfBeesInGroup.ToString();
						// Match the size of the sprite object
						RectTransform spriteRectTransform = spriteObject.GetComponent<RectTransform>();
						RectTransform tmpRectTransform = tmp.GetComponent<RectTransform>();

						if (spriteRectTransform != null && tmpRectTransform != null)
						{
							tmpRectTransform.sizeDelta = spriteRectTransform.sizeDelta;
							tmpRectTransform.position = spriteRectTransform.position;
						}
					}
				}
			}
		}
		return sprites;
	}

	public void ReGetSprites()
	{
		// Clear existing icons
		foreach (Transform child in layoutGroup)
		{
			Destroy(child.gameObject);
		}

		// Clear the spriteObjects list
		spriteObjects.Clear();

		// Get new sprites and instantiate new icons
		Sprites = GetSprites();
		foreach (Sprite sprite in Sprites)
		{
			GameObject newSprite = Instantiate(spritePrefab, layoutGroup);
			newSprite.GetComponent<Image>().sprite = sprite;
			spriteObjects.Add(newSprite); // Store the reference
		}
	}

	private static TextMeshProUGUI GetIconText(GameObject spriteObject)
	{
		// Create a new GameObject for the TextMeshPro
		GameObject textObject = new GameObject("TextMeshProObject");
		textObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		textObject.transform.SetParent(spriteObject.transform, false); // Set false to keep local scale and position
		textObject.transform.localPosition = Vector3.zero; // Adjust the position as needed

		// Add a TextMeshPro component to the GameObject
		TextMeshProUGUI tmp = textObject.AddComponent<TextMeshProUGUI>();
		tmp.alignment = TextAlignmentOptions.Center;
		tmp.text = "";
		return tmp;
	}

	private GameObject GetBeeIcon(Sprite sprite, Transform item)
	{
		// Create a new GameObject for the sprite
		GameObject spriteObject = new GameObject("SpriteObject");
		spriteObject.transform.SetParent(layoutGroup, false); // Set false to keep local scale and position

		// Add an Image component to the GameObject
		Image image = spriteObject.AddComponent<Image>();
		image.sprite = sprite;
		return spriteObject;
	}
	int storedIndex = -1;
	public void SetSpriteSelected(int index)
	{
		if (index >= 0 && index < spriteObjects.Count)
		{
			GameObject spriteObject = spriteObjects[index];
			Image image = spriteObject.GetComponent<Image>();
			if (image != null)
			{
				image.sprite = Sprite.Create(selecticon, new Rect(0, 0, selecticon.width, selecticon.height), new Vector2(0.5f, 0.5f));
				if (storedIndex != index)
				{
					UnSetSpriteSelected(storedIndex);
				}
				storedIndex = index;
			}
			else
			{
				Debug.LogError("Image component not found on the GameObject.");
			}
		}
		else
		{
				Debug.LogError("Index out of range.");
		}
	}
	
	public void SetNewSpriteSelected(int index)
	{
		if (index >= 0 && index < spriteObjects.Count)
		{
			GameObject spriteObject = spriteObjects[index];
			Image image = spriteObject.GetComponent<Image>();
			if (image != null)
			{
				image.sprite = Sprite.Create(selecticon, new Rect(0, 0, selecticon.width, selecticon.height), new Vector2(0.5f, 0.5f));
				storedIndex = index;
			}
			else
			{
				Debug.LogError("Image component not found on the GameObject.");
			}
		}
		else
		{
			Debug.LogError("Index out of range.");
		}
	}

	public void UnSetSpriteSelected(int index)
	{
		if (index >= 0 && index < spriteObjects.Count)
		{
			GameObject spriteObject = spriteObjects[index];
			Image image = spriteObject.GetComponent<Image>();
			if (image != null)
			{
				image.sprite = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), new Vector2(0.5f, 0.5f));
			}
			else
			{
				Debug.LogError("Image component not found on the GameObject.");
			}
		}
		else
		{
			Debug.LogError("Index out of range.");
		}
	}

	/*
	public void ChangeSprite(GameObject spriteObject, Sprite newSprite, int index)
	{
		Image image = spriteObject.GetComponent<Image>();
		if (image != null)
		{
			image.sprite = newSprite;
		}
		else
		{
			Debug.LogError("Image component not found on the GameObject.");
		}
	}
	*/
	internal void HelloLPanel()
	{
		Debug.Log("LPanel says HELLO!");
	}
}