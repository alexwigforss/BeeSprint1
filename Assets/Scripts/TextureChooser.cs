using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChooser : MonoBehaviour
{
	public Texture2D[] textures; // Array of your imported textures

	void Start()
	{
		Material material = GetComponent<Renderer>().material;
		if (textures.Length > 0)
		{
			int randomIndex = Random.Range(0, textures.Length);
			material.mainTexture = textures[randomIndex];
		}
	}
}
