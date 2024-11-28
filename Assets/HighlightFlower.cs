using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightFlower : MonoBehaviour
{
	[SerializeField]
	Material normalbasemat;
	[SerializeField]
	Material highlightbasemat;

	// Method to highlight the selected flower
	public void HighlightSelected()
	{
		foreach (Transform flower in transform)
		{
			Transform grass = flower.Find("Grass");
			if (grass != null)
			{
				Renderer grassRenderer = grass.GetComponent<Renderer>();
				if (grassRenderer != null)
				{
					grassRenderer.material = highlightbasemat;
				}
			}
		}
	}

	// Method to unhighlight the selected flower
	public void UnlightSelected()
	{
		foreach (Transform flower in transform)
		{
			Transform grass = flower.Find("Grass");
			if (grass != null)
			{
				Renderer grassRenderer = grass.GetComponent<Renderer>();
				if (grassRenderer != null)
				{
					grassRenderer.material = normalbasemat;
				}
			}
		}
	}
}

