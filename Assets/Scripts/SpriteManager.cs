using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour
{
	public GameObject spritePrefab;
	public Transform layoutGroup;
	public Transform flowerSpawnersParent; // Assign this in the Inspector
	int spritesadded = 0;
	void Start()
	{
		List<Sprite> sprites = GetSpritesFromHierarchy();
		foreach (Sprite sprite in sprites)
		{
			GameObject newSprite = Instantiate(spritePrefab, layoutGroup);
			newSprite.GetComponent<Image>().sprite = sprite;
		}
	}

	List<Sprite> GetSpritesFromHierarchy()
	{
		List<Sprite> sprites = new List<Sprite>();

		foreach (Transform SpawnLocation in flowerSpawnersParent)
		{
			Transform flowerSpawner = SpawnLocation.Find("FlowerSpawner");
			if (flowerSpawner != null)
			{
				flowerSpawner spawnerScript = flowerSpawner.GetComponent<flowerSpawner>();
				if (spawnerScript != null)
				{
					Texture2D texture = spawnerScript.texture as Texture2D;

					//Sprite sprite = spawnerScript.GetComponent<SpriteRenderer>().sprite;
					if (texture != null)
					{
						Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
						sprites.Add(sprite);
						spritesadded++;
					}
				}
			}
		}
		Debug.Log(spritesadded + " Sprites added");
		return sprites;
	}
}