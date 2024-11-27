using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hitzones : MonoBehaviour
{
	// https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/
	private static List<Transform> hitList = new List<Transform>(); // MFD
	private static Dictionary<int, List<Transform>> hitPositions = new Dictionary<int, List<Transform>>();

	// MFD
	public static int PtrintHitListCount()
	{
		// Debug.Log("Nr of hitzones = " + hitList.Count);
		return hitList.Count;
	}

	public static int noOfHitzonesInSpecie(int id)
	{
		// Debug.Log("Nr of hitzones = " + hitList.Count);
		return HitPositions[id].Count;
	}

	public static int noOfSpecies()
	{
		// Debug.Log("Nr of species = " + HitPositions.Count);
		return HitPositions.Count;
	}

	public static List<Transform> HitList => hitList; // MFD
	public static Dictionary<int, List<Transform>> HitPositions { get => hitPositions; set => hitPositions = value; }

	public static bool Contain(Transform t) {
        if (hitList.Contains(t))
        {
			return true;
        }
		return false;
    }

}
