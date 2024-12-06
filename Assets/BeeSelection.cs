using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSelection : MonoBehaviour
{
	[SerializeField] GameObject ssphere;

	void Start()
	{
		ssphere.SetActive(false);
	}

	/// <summary>
	/// Returns False if already active
	/// </summary>
	/// <returns></returns>
	public void EnableSphere()
	{
		//if (ssphere.activeSelf == true)
		//{
		//	return false;
		//}
		ssphere.SetActive(true);
		//return true;
	}
	public void DisableSphere()
	{
		ssphere.SetActive(false);
	}

}
