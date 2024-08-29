using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollision : MonoBehaviour
{
	private flowerSpawner parentScript;

	void Start()
	{
		// Get the parent script component
		parentScript = GetComponentInParent<flowerSpawner>();

		if (parentScript == null)
		{
			Debug.LogError("flowerSpawner component not found in parent hierarchy.");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Triggered Something");

		if (parentScript != null)
		{
			// Notify the parent about the trigger event
			parentScript.OnChildTrigger();
		}
		else
		{
			Debug.LogError("Cannot call OnChildTrigger because parentScript is null.");
		}
	}
}
