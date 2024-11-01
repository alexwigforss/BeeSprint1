using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class Menu : MonoBehaviour
{
	public GameObject spritePrefab;
	public Transform layoutGroup;
	public Transform flowerSpawnersParent; // Assign this in the Inspector
	internal int spritesadded = 0;
	void Start()
	{
		List<Sprite> sprites = GetSprites();
		foreach (Sprite sprite in sprites)
		{
			GameObject newSprite = Instantiate(spritePrefab, layoutGroup);
			newSprite.GetComponent<Image>().sprite = sprite;
		}
	}

	protected virtual List<Sprite> GetSprites()
	{
		List<Sprite> sprites = new List<Sprite>();
		return sprites;
	}

}
