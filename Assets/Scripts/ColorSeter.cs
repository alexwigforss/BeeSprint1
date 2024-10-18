using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSeter : MonoBehaviour
{
	public Material targetMaterial;
	Color customColor;
	// Start is called before the first frame update
	void Start()
    {
		// Get the Renderer component from this object
		var renderer = GetComponent<Renderer>();
		//Color customColor = new(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		//Color customColor = new(0.2f, 0.0f, 0.0f);
		//// Set the color of the material
		//targetMaterial.SetColor("_Color", customColor); // Change to your desired color

		// Call SetColor using the shader property name "_Color" and set the color to red
		renderer.material.SetColor("_Color", customColor);
	}
		
    // Update is called once per frame
    void Update()
    {
        
    }
}
