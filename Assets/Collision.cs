using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml;

public class Collision : MonoBehaviour
{
	int pollen = 0;
	private TMP_Text tmpText;
	// Start is called before the first frame update
	void Start()
	{
		Debug.Log(transform.childCount);
		tmpText = GetComponentInChildren<Canvas>().GetComponentInChildren<TMP_Text>();
	}

	// Update is called once per frame
	void Update()
	{

	}
	public void OnCollisionEnter(UnityEngine.Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Flapper>().enabled = false;
			transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Flapper>().enabled = false;
		}
    }
	public void OnTriggerEnter(Collider other)
	{
		pollen++;
		Debug.Log("Pollen = " + pollen);
		// Set the text
		if (tmpText != null)
		{
			tmpText.text = "" + pollen;
		}
		else
		{
			Debug.LogError("TMP_Text component not found!");
		}
	}
	public void OnCollisionExit(UnityEngine.Collision collision)
	{
		transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Flapper>().enabled = true;
		transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Flapper>().enabled = true;
	}
}