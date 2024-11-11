using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml;
using System;

public class Collision : MonoBehaviour
{
	// public Resources resourceManager;
	int pollen = 0;
	int water = 0;
	private TMP_Text tmpText;

	bool hasLeft = false;
	bool hasLeftNest = false;
	private Coroutine decreaseCoroutine;
	private Coroutine increaseCoroutine;

	// Start is called before the first frame update
	void Start()
	{
		// resourceManager = FindObjectOfType<Resources>();
		//Debug.Log(transform.childCount);
		tmpText = GetComponentInChildren<Canvas>().GetComponentInChildren<TMP_Text>();
	}

	// Update is called once per frame
	void Update()
	{

	}
	public void OnCollisionEnter(UnityEngine.Collision collision)
	{
		if (collision.gameObject.name.Equals("World"))
		{
			transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Flapper>().enabled = false;
			transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Flapper>().enabled = false;
		}
        else if (collision.gameObject.name.Equals("Water"))
        {
			hasLeft = false;
			increaseCoroutine ??= StartCoroutine(CollectWaterOverTime());
			//Debug.Log("SPLASH water hit ");
		}
	}

	public void OnCollisionExit(UnityEngine.Collision collision)
	{
		if (collision.gameObject.name.Equals("World"))
		{
			transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Flapper>().enabled = true;
			transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Flapper>().enabled = true;
		}
		else if (collision.gameObject.name.Equals("Water"))
		{
			hasLeft = true;
			if (increaseCoroutine != null)
			{
				StopCoroutine(increaseCoroutine);
				increaseCoroutine = null;
			}
			//Debug.Log("SPLASH water hit ");
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("FlowerHitZone"))
		{
			pollen++;
			UpdatePlayerText();
			Debug.Log("Drone collected pollen " + pollen);
		}
		else if (other.CompareTag("Nest"))
		{
			hasLeftNest = false;
			decreaseCoroutine ??= StartCoroutine(UnloadResources());
		}
    }

	public void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Nest"))
		{
			hasLeftNest = true;
			if (decreaseCoroutine != null)
			{
				StopCoroutine(decreaseCoroutine);
				decreaseCoroutine = null;
			}
		}
	}

	private void UpdatePlayerText()
	{
		if (tmpText != null)
		{
			tmpText.text = "" + pollen + "," + water;
		}
		else
		{
			Debug.LogError("TMP_Text component not found!");
		}
	}

	IEnumerator CollectWaterOverTime()
	{
		Debug.Log("Increasing" + water);
		while (water < 10)
		{
			if (hasLeft)
			{
				hasLeft = false;
				break;
			}
			water++;
			UpdatePlayerText();
			Debug.Log("Post Increasing" + water);
			yield return new WaitForSeconds(1);
		}
		increaseCoroutine = null;
	}

	IEnumerator UnloadResources()
	{
		while (pollen > 0 || water > 0)
		{
			if (hasLeftNest)
			{
				hasLeftNest = false;
				break;
			}
            if (pollen > 0)
            {
				pollen--;
				Resources.pol++;
            }
            if (water > 0)
            {
				water--;
				Resources.wat++;
            }
			UpdatePlayerText();
			yield return new WaitForSeconds(1);
		}
		decreaseCoroutine = null;
	}


}