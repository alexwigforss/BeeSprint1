using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitzones : MonoBehaviour
{

	public static List<Transform> hitList = new List<Transform>();

	public static int PtrintHitListCount()
	{
		Debug.Log("Nr of hitzones = " + hitList.Count);
		return hitList.Count;
	}
	public static bool Contain(Transform t) {
        if (hitList.Contains(t))
        {
			return true;
        }
		return false;
    }

}
