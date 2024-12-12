using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightFlower : MonoBehaviour
{
	public bool selected = false;
	[SerializeField]
	Material normalbasemat;
	[SerializeField]
	Material highlightbasemat;

	// Method to highlight the selected flower
	public bool HighlightSelected()
	{
		if (selected)
		{
			UnlightSelected();
			return false;
		}
		selected = true;
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
		return true;
	}

	// Method to unhighlight the selected flower
	public void UnlightSelected()
	{
		selected = false;
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

