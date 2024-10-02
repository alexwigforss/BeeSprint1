using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
	public GameObject spritePrefab; // Assign your prefab here
	public Transform layoutGroup; // Assign your layout group here
	public Sprite[] sprites; // Assign your list of sprites here
							 // Start is called before the first frame update
	void Start()
    {
		foreach (Sprite sprite in sprites)
		{
			GameObject newSprite = Instantiate(spritePrefab, layoutGroup);
			newSprite.GetComponent<Image>().sprite = sprite;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
