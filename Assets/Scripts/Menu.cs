using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// NOTE This is an interface to be extended by the side panels
//		Therefore it is not atached to any gameobject in the editor
public abstract class Menu : MonoBehaviour {
	public GameObject spritePrefab;
	public Transform layoutGroup;
	public Transform spawnersParent;
	List<Sprite> sprites;
	public List<Sprite> Sprites { get => sprites; set => sprites = value; }

	protected virtual void Start() {
		Sprites = GetSprites();
		foreach (Sprite sprite in Sprites) {
			GameObject newSprite = Instantiate(spritePrefab, layoutGroup);
			newSprite.GetComponent<Image>().sprite = sprite;
		}
	}

	protected virtual List<Sprite> GetSprites() {
		List<Sprite> sprites = new();
		return sprites;
	}
}
