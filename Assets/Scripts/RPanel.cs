using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPanel : Menu
{
	public List<Sprite> sprites = new List<Sprite>();

	private void Start()
	{
		base.Start();
	}
	protected override List<Sprite> GetSprites()
	{
		foreach (Transform SpawnLocation in spawnersParent)
		{
			Transform flowerSpawner = SpawnLocation.Find("FlowerSpawner");
			if (flowerSpawner != null)
			{
				if (flowerSpawner.TryGetComponent<FlowerSpawner>(out var spawnerScript))
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
