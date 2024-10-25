using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.GraphicsBuffer;

public class Beehave : MonoBehaviour
{
	[SerializeField]
	Transform goal;
	Transform stashedGoal;
	[SerializeField]
	Collider goalCollider;
	AutoMove engine;
	Realigner re;
	bool closer = false;
	bool selected = true;
	public Transform HiveLocation;
	Transform target;
	private float prioDist;
	private float postDist;

	bool aligned = false;

	private bool fwd = true;
	int state;
	float timer = 0f;
	float second, twosec = 0f;
	int turndirection = 0;
	private enum States
	{
		home,
		idle,
		search,
		collect
	}
	// Start is called before the first frame update
	void Start()
	{
		target = goal.transform;
		engine = GetComponent<AutoMove>();
		re = GetComponent<Realigner>();
		engine.ResetAll();
		postDist = Vector3.Distance(transform.position, goal.transform.position);
		state = (int)States.search;
	}

	// Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime;
		twosec += Time.deltaTime;
		switch (state)
		{
			case (int)States.home:
				aligned = false;
				transform.position = HiveLocation.transform.position;
				break;
			case (int)States.idle:
				if (!aligned) { aligned = re.AlignXZ(true); }
				if (timer >= 0.8f) { engine.MoveForward(true); }
				else { engine.MoveForward(false); }
				engine.MoveRight(true, 2);
				break;

			case (int)States.search:
				if (!aligned) { aligned = re.AlignXZ(true); }
				engine.MoveForward(true);
				if (turndirection == 0)
				{
					engine.MoveRight(true);
				}
				else
				{
					engine.MoveLeft(true);
				}
				if (twosec >= 2f) { RandomDirection(); }
				break;
			case (int)States.collect:
				MoveTowards(target);
				break;
			default:
				Debug.Log("Not moving!");
				break;
		}
		if (second >= 1f) { second -= 1f; }
		//if (twosec >= 2f) { twosec -= 2f; }
		//if (timer > 2)
		//{
		//	state = (int)States.collect;
		//}
		//while (true)
		//{
		//	Debug.Log("hoolding");
		//}
	}
	public void OnTriggerEnter(Collider other)
	{
		if (other == goalCollider)
		{
			stashedGoal = goal;
			goal = HiveLocation;
			Debug.Log("Colliding with goalCollider!");
		}
		else if (other.CompareTag("Nest"))
		{
			goal = stashedGoal;
		}
		target = goal.transform;

	}

	private void MoveTowards(Transform t)
	{
		prioDist = postDist;
		postDist = Vector3.Distance(transform.position, t.position);
		// Debug.Log("Distance Before " + prioDist + " Distance After " + postDist);
		//engine.rotateTowards(t.position);
		engine.rotateTowards(t.position);
		engine.MoveForward(true);
		engine.rotateTowards(t.position);
		if (postDist < 0.9)
		{
			engine.rotateTowards(t.position * 8);
		}
	}

	private void RandomDirection()
	{
		Debug.Log("Change Direction");
		turndirection = Random.Range(0, 2);
		engine.MoveRight(false);
		engine.MoveLeft(false);
		twosec -= 2f;
	}

	private void LateUpdate()
	{
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
