using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitzones : MonoBehaviour
{

	public static Queue<Transform> hitList = new Queue<Transform>();

	public static void PtrintHitListCount()
	{
		//Debug.Log("Nr of hitzones = " + hitList.Count);
	}
}
