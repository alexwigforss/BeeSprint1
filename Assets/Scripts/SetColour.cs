using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColour : MonoBehaviour {
	[SerializeField]
	Material material;

	void Start() {
		Transform parent = transform.parent;
		Transform headTransform = parent.Find("Head");
		if (headTransform != null) {
			Renderer headRenderer = headTransform.GetComponent<Renderer>();
			if (headRenderer != null) {
				Color headColor = headRenderer.material.GetColor("_Color");
				// Set the color to your material
				material.SetColor("_Color", headColor);
				// Get the texture from the Head's material
				Texture2D texture = headRenderer.material.mainTexture as Texture2D;
				if (texture != null) {
					Color color = texture.GetPixel(132, 33);
					Material newMaterial = new Material(material);
					newMaterial.SetColor("_Color", color);
					// Assign the new material to this object's renderer
					Renderer thisRenderer = GetComponent<Renderer>();
					if (thisRenderer != null) {
						thisRenderer.material = newMaterial;
					}
				}
			}
		}
	}
}
