using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Hitzones : MonoBehaviour {
	// https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/
	private static Dictionary<int, List<Transform>> hitPositions = new Dictionary<int, List<Transform>>();
	public static Dictionary<int, List<Transform>> HitPositions { get => hitPositions; set => hitPositions = value; }

	public static int noOfHitzonesInSpecie(int id) {
		// Debug.Log("Nr of hitzones = " + hitList.Count);
		return HitPositions[id].Count;
	}

	public static int noOfSpecies() {
		// Debug.Log("Nr of species = " + HitPositions.Count);
		return HitPositions.Count;
	}

	public static int PtrintHitListCount() {
		// Debug.Log("Nr of hitzones = " + hitList.Count);
		return hitPositions.Count;
	}

	public static bool Contain(Transform t) {
		foreach (var kvp in hitPositions) {
			if (kvp.Value.Contains(t)) {
				return true;
			}
		}
		return false;
	}
}
