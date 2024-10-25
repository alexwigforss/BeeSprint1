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
	private TMP_Text tmpText;

	bool hasLeftNest = false;
	private Coroutine decreaseCoroutine;

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
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Flapper>().enabled = false;
			transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Flapper>().enabled = false;
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
			decreaseCoroutine ??= StartCoroutine(DecreaseVariableOverTime());
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
			tmpText.text = "" + pollen;
		}
		else
		{
			Debug.LogError("TMP_Text component not found!");
		}
	}

	IEnumerator DecreaseVariableOverTime()
	{
		while (pollen > 0)
		{
			if (hasLeftNest)
			{
				hasLeftNest = false;
				break;
			}
			pollen--;
			UpdatePlayerText();
			Resources.resources++;
			yield return new WaitForSeconds(1);
		}
		decreaseCoroutine = null;
	}

	public void OnCollisionExit(UnityEngine.Collision collision)
	{
		transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Flapper>().enabled = true;
		transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Flapper>().enabled = true;
	}
}