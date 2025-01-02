using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectiveMemory : MonoBehaviour {
	// Chosed HashSet to avoid duplicate values
	// public List<int> memory = new List<int>();
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
		string result = string.Join(", ", memory);
		Debug.Log(result);
	}
}
