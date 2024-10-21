using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.GraphicsBuffer;

public class Beehave : MonoBehaviour
{
	[SerializeField]
	Transform goal = null;
	AutoMove engine = new();
	Realigner re = new();
	// bool tilted = false;

	public Transform HiveLocation;

	// Start is called before the first frame update
	void Start()
	{
		engine = GetComponent<AutoMove>();
		re = GetComponent<Realigner>();
		engine.ResetAll();
	}

	// Update is called once per frame
	void Update()
	{
		engine.MoveForward(true);
		// engine.TurnTowards(goal);
		// Determine the direction to the target
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * 1.0f);

		//else
		//{
		//	if (!re.IsXzAligned())
		//	{
		//		re.AlignXZ(true);
		//	}
		//}
	}
}
