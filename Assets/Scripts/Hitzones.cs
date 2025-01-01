using System.Collections.Generic;
using UnityEngine;

public class Hitzones : MonoBehaviour {
	// https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/
	private static Dictionary<int, List<Transform>> hitPositions = new Dictionary<int, List<Transform>>();
	public static Dictionary<int, List<Transform>> HitPositions { get => hitPositions; set => hitPositions = value; }

	public static int noOfHitzonesInSpecie(int id) {
		return HitPositions[id].Count;
	}

	public static int noOfSpecies() {
		return HitPositions.Count;
	}

	public static int PtrintHitListCount() {
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
