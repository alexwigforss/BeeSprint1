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
					Color[] colors = texture.GetPixels();
					List<Color> uniqueColors = new List<Color>();

					foreach (Color color in colors)
					{
						if (!uniqueColors.Contains(color))
						{
							uniqueColors.Add(color);
						}
					}

					Debug.Log("Unique colors count: " + uniqueColors.Count);
					material.SetColor("_Color", uniqueColors[100]);
				}


			}
		}
	}
}
