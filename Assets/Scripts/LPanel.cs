using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPanel : Menu
{
	[SerializeField]
	Texture2D icon;
	protected override List<Sprite> GetSprites()
	{
		List<Sprite> sprites = new List<Sprite>();

		foreach (Transform bee in spawnersParent)
		{
			//Transform bee = spawnLocation.Find("Bee");
			if (bee != null)
			{
				Debug.Log("BAMMM BEE");
				//beeSpawner spawnerScript = beeSpawner.GetComponent<bee>();
				//if (spawnerScript != null)
				//{
				Texture2D texture = icon;

				//Sprite sprite = spritePrefab.GetComponent<Sprite>();
				if (texture != null)
				{
					Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
					sprites.Add(sprite);
					spritesadded++;
				}
				//}
			}
		}
		// Debug.Log(spritesadded + " Sprites added");
		return sprites;
	}
}
