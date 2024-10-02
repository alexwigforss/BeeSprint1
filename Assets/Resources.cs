using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public static int resources;
    int presresource;

	[SerializeReference]
	public TMP_Text resourceText;
	// Start is called before the first frame update
	void Start()
    {
		resources = 0;
        presresource = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (resources != presresource)
        {
            Debug.Log(resources);
            presresource = resources;
			UpdateResourceText();

		}
    }

	private void UpdateResourceText()
	{
		if (resourceText != null)
		{
			resourceText.text = "" + resources;
		}
		else
		{
			Debug.LogError("TMP_Text component not found!");
		}
	}
}
