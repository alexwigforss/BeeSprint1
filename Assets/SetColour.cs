using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColour : MonoBehaviour
{
	[SerializeField]
	Material material;

	void Start()
	{
		// Find the parent object
		Transform parent = transform.parent;

		// Find the Head object under the same parent
		Transform headTransform = parent.Find("Head");
		if (headTransform != null)
		{
			Renderer headRenderer = headTransform.GetComponent<Renderer>();
			if (headRenderer != null)
			{
				// Get the color from the Head's material
				Color headColor = headRenderer.material.GetColor("_Color");

				// Set the color to your material
				material.SetColor("_Color", headColor);

				
				// Get the texture from the Head's material
				Texture2D texture = headRenderer.material.mainTexture as Texture2D;
				if (texture != null)
				{
					// Get all colors from the texture
					Debug.Log("Texture dimensions: " + texture.width + " , " + texture.height);

					Color color = texture.GetPixel(132,33);
					//Color colors = texture.GetPixels(132,66);

					//List<Color> uniqueColors = new List<Color>();

					Material newMaterial = new Material(material);
					newMaterial.SetColor("_Color", color);

					// Assign the new material to this object's renderer
					Renderer thisRenderer = GetComponent<Renderer>();
					if (thisRenderer != null)
					{
						thisRenderer.material = newMaterial;
					}
				}


			}
		}
	}
}
