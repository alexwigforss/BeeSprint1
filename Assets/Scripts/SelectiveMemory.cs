using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectiveMemory : MonoBehaviour
{
	// Chosed HashSet to avoid duplicatre values
    //public List<int> memory = new List<int>();
	private HashSet<int> memory = new HashSet<int>();

	public void AddSpecie(int value)
	{
		memory.Add(value);
	}

	public void RemoveSpecie(int value)
	{
		memory.Remove(value);
	}

	public bool ContainsSpecie(int value)
	{
		return memory.Contains(value);
	}

	public void PrintSpecies()
	{
		string result = string.Join(", ", memory); Debug.Log(result);
	}
}
