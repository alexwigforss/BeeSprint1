using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectiveMemory : MonoBehaviour, ISelectiveMemory {
	private HashSet<int> memory;

	private void Awake() {
		memory = new HashSet<int>();
	}

	public void AddSpecie(int value) {
		memory.Add(value);
	}

	public void RemoveSpecie(int value) {
		memory.Remove(value);
	}

	public bool ContainsSpecie(int value) {
		return memory.Contains(value);
	}

	public int[] GetSpecies() {
		return memory.ToArray();
	}

	public void PrintSpecies() {
		foreach (var specie in memory) {
			Debug.Log(specie);
		}
	}
}
