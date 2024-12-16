using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSelection : MonoBehaviour
{
	[SerializeField] GameObject ssphere;

	void Start()
	{
		if (ssphere != null)
		{
			ssphere.SetActive(false);
		}
		else
		{
			Debug.LogWarning("ssphere is not assigned in the Inspector.");
		}
	}

	/// <summary>
	/// Returns False if already active
	/// </summary>
	/// <returns></returns>
	public void EnableSphere()
	{
		if (ssphere != null)
		{
			ssphere.SetActive(true);
			Debug.Log("ssphere has been enabled.");
		}
		else
		{
			Debug.LogWarning("ssphere is not assigned in the Inspector.");
		}
	}

	public void DisableSphere()
	{
		if (ssphere != null)
		{
			ssphere.SetActive(false);
			Debug.Log("ssphere has been disabled.");
		}
		else
		{
			Debug.LogWarning("ssphere is not assigned in the Inspector.");
		}
	}
}

