using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPanel : Menu
{
	protected override List<Sprite> GetSprites()
	{
		List<Sprite> sprites = new List<Sprite>();

		foreach (Transform SpawnLocation in spawnersParent)
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
		//Debug.Log(spritesadded + " Sprites added");
		return sprites;
	}
}
