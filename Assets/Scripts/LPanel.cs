using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LPanel : Menu
{
	[SerializeField] Texture2D icon;

	protected override List<Sprite> GetSprites()
	{
		List<Sprite> sprites = new List<Sprite>();

		foreach (Transform bee in spawnersParent)
		{
			if (bee != null)
			{
				Debug.Log("BAMMM BEE");
				Texture2D texture = icon;

				if (texture != null)
				{
					Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
					sprites.Add(sprite);

					// Create a new GameObject for the sprite
					GameObject spriteObject = new GameObject("SpriteObject");
					spriteObject.transform.SetParent(layoutGroup, false); // Set false to keep local scale and position

					// Add an Image component to the GameObject
					Image image = spriteObject.AddComponent<Image>();
					image.sprite = sprite;

					// Create a new GameObject for the TextMeshPro
					GameObject textObject = new GameObject("TextMeshProObject");
					textObject.transform.SetParent(spriteObject.transform, false); // Set false to keep local scale and position
					textObject.transform.localPosition = Vector3.zero; // Adjust the position as needed
					
					// Add a TextMeshPro component to the GameObject
					TextMeshProUGUI tmp = textObject.AddComponent<TextMeshProUGUI>();
					tmp.text = "0"; // Set your desired label text here
					tmp.alignment = TextAlignmentOptions.Center;
					spritesadded++;
				}
			}
		}

		return sprites;
	}
}