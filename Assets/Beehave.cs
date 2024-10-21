using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.GraphicsBuffer;

public class Beehave : MonoBehaviour
{
	[SerializeField]
	Transform goal;// = null;
	AutoMove engine;// = AddCo;
	Realigner re;
	// bool tilted = false;

	public Transform HiveLocation;

	private float prioDist;
	private float postDist;

	private bool fwd = true;

	// Start is called before the first frame update
	void Start()
	{
		engine = GetComponent<AutoMove>();
		re = GetComponent<Realigner>();
		engine.ResetAll();
		prioDist = Vector3.Distance(transform.position, goal.transform.position);

	}

	// Update is called once per frame
	void Update()
	{

		engine.rotateTowards(goal.transform.position);
        if (fwd)
        {
			engine.MoveForward(true);
        }
	}

	private void LateUpdate()
	{
		postDist = Vector3.Distance(transform.position, goal.transform.position);
		Debug.Log("Distance Before " + prioDist + " Distance After " + postDist);
  //      if (postDist < prioDist)
  //      {
		//	fwd = true;
		//	return;
  //      }
		//fwd = false;
    }
}
		//else
		//{
		//	if (!re.IsXzAligned())
		//	{
		//		re.AlignXZ(true);
		//	}
		//}
