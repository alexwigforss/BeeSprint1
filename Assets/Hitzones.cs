using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitzones : MonoBehaviour
{
	public static List<KeyValuePair<int, List<Transform>>> hitList = new List<KeyValuePair<int, List<Transform>>>();
	// TODO At start append keys by number
	// TODO At hitzone enable add transformation
	// TODO At hitzone dissable remove transformation


	public GameObject flowerSpawners;
	
	void Start()
	{
		StartCoroutine(LateStart());

	}

	IEnumerator LateStart()
	{
		yield return new WaitForEndOfFrame(); // Wait until the end of the frame myVariable = 42; // Set your variable here
		foreach (Transform child in flowerSpawners.GetComponentsInChildren<Transform>())
		{
			// Perform your operations on each child object here
			Debug.Log("Child object: " + child.name);
		}
	} 

		// Update is called once per frame
		void Update()
    {
        
    }
}
