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
			if (bee != null)
			{
				Debug.Log("BAMMM BEE");
				Texture2D texture = icon;

				if (texture != null)
				{
					Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
					sprites.Add(sprite);
					spritesadded++;
				}
			}
		}
		return sprites;
	}
}
