using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitzones : MonoBehaviour
{

	public static List<Transform> hitList = new List<Transform>();

	public static void PtrintHitListCount()
	{
		Debug.Log("Nr of hitzones = " + hitList.Count);
	}
}
