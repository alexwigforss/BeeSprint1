using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitzones : MonoBehaviour
{

	public static Queue<Transform> hitList = new Queue<Transform>();

	// public GameObject flowerSpawners;
	
	void Start()
	{
		//foreach (Transform child in flowerSpawners.GetComponentsInChildren<Transform>())
		//{
		//	// Perform your operations on each child object here
		//	Debug.Log("Child object: " + child.name);
		//}

		//StartCoroutine(LateStart());

	}

	//IEnumerator LateStart()
	//{
	//	yield return new WaitForEndOfFrame(); // Wait until the end of the frame myVariable = 42; // Set your variable here
	//	foreach (Transform child in flowerSpawners.GetComponentsInChildren<Transform>())
	//	{
	//		// Perform your operations on each child object here
	//		Debug.Log("Child object: " + child.name);
	//	}
	//}
	public static void PtrintHitListCount()
	{
		Debug.Log("Nr of hitzones = " + hitList.Count);
	}
}
