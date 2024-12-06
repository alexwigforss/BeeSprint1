using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LPanel : Menu
{
	[SerializeField] Texture2D icon;
	[SerializeField] Texture2D selecticon;
	
	protected override List<Sprite> GetSprites()
	{
		List<Sprite> sprites = new List<Sprite>();
		TextMeshProUGUI tmp = new TextMeshProUGUI();
		Texture2D texture = icon;
		if (texture != null)
		{
			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			foreach (Transform item in spawnersParent)
			{
				if (item != null)
				{
					//if (item.name.Contains("Bee"))
					//{
					//	GameObject spriteObject = GetBeeIcon(sprite, item);
					//	tmp = GetIconText(spriteObject);
					//	spritesadded++;

					//}
					if(item.name.Contains("Group"))
					{
						int noOfBeesInGroup = 0;
						GameObject spriteObject = GetBeeIcon(sprite, item);
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

	public void ChangeToIconSelected() 
	{

	}
}