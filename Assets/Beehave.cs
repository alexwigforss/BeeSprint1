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
	//private float prioDist;
	private float postDist;

	bool aligned = false;
	bool unloading = false;
	private bool fwd = true;
	public int state;
	float timer = 0f;
	float twosec = 0f;
	int turndirection = 0;


	public int selectedSpecie = 0;
	int goalItterator = 0;
	List<Transform> internalHitList = new List<Transform>();

	Collision collision;

	private enum States
	{
		home,
		idle,
		search,
		collect,
		water,
		propol,
		unload
	}

	void Start()
	{
		target = goal.transform;
		engine = GetComponent<AutoMove>();
		re = GetComponent<Realigner>();
		collision = GetComponent<Collision>();
		engine.ResetAll();
		postDist = Vector3.Distance(transform.position, goal.transform.position);
		state = (int)States.search;
	}

	private void OnValidate()
	{
		if (Application.isPlaying)
		{
			getGoalList(selectedSpecie);
		}
	}

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
				if (turndirection == 0) { engine.MoveRight(true); }
				else { engine.MoveLeft(true); }
				if (twosec >= 2f) { RandomDirection(); }
				if (Hitzones.HitList.Count > 0)
				{
					getGoalList(selectedSpecie);
					getNextGoal();
					target = goal;
					state = (int)States.collect;
				}
				break;
			case (int)States.collect:
				// Debug.Log("BAM COLLECT");
				if (target != null && target.gameObject.activeInHierarchy)
				{
					MoveTowards(target);
				}
				else
				{
					getGoalList(selectedSpecie);
					getNextGoal();
					//getNextGoal(); // Temp Fix for the attempt to collect dead hitzone bug.
					target = goal;
				}
				if (collision.totalLoad >= collision.maxload)
				{
					// unloading = true;
					target = HiveLocation;
					state = (int)States.unload;
				}
				break;
			case (int)States.unload:
				if (collision.totalLoad > 0)
				{
					target = HiveLocation;
					MoveTowards(target);
				}
				else
				{
					getGoalList(selectedSpecie);
					getNextGoal();
					target = goal;
					state = (int)States.collect;
				}
				break;
			default:
				Debug.Log("Not moving!");
				break;
		}
	}

	private void getGoalList(int selector)
	{
		// Debug.Log("Getting HitZone List");
		try
		{
			if (selector == 0)
			{
				internalHitList = Hitzones.HitList;
			}
			else if (selector > 0)
			{
				internalHitList = Hitzones.HitPositions[selector];
				if (internalHitList.Count > 4)
				{
					internalHitList.RemoveRange(0, internalHitList.Count / 2);
				}
			}
		}
		catch (System.Exception e)
		{
			Debug.LogException(e);
		}
	}

	private void getNextGoal()
	{
		if (internalHitList.Count > 0)
		{
			if (goalItterator < internalHitList.Count - 1)
			{
				goalItterator++;
			}
			else
			{
				goalItterator = 0;
			}
			goal = internalHitList[goalItterator];
			// Debug.Log("Getting Goal, hit list size = " + internalHitList.Count);
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		// TBD Implement return to home when fully loaded.
		//if (other == goalCollider)
		if (other.tag == "FlowerHitZone")
		{
			if (internalHitList.Count > 0)
			{
				getNextGoal();
			}
			else
			{
				// Debug.Log("hitListWas ZERO");
				stashedGoal = goal;
				goal = HiveLocation;
			}
		}
		else if (other.CompareTag("Nest"))
		{
			// Debug.Log("Hit nest");
			getGoalList(selectedSpecie);
			getNextGoal();
		}
		target = goal.transform;

	}

	private void MoveTowards(Transform t)
	{
		//prioDist = postDist;
		postDist = Vector3.Distance(transform.position, t.position);
		engine.rotateTowards(t.position);
		engine.MoveForward(true);
		engine.rotateTowards(t.position);
		if (postDist < 0.9)
		{
			engine.rotateTowards(t.position * 8);
			// Hack wich i hope prevent from chasing dead targets
			if (!Hitzones.Contain(t))
			{
				// Debug.Log("Trying to reach dead hitzone");
				getGoalList(selectedSpecie);
				getNextGoal();
			}
		}
	}

	private void RandomDirection()
	{
		// Debug.Log("Change Direction");
		turndirection = Random.Range(0, 2);
		engine.MoveRight(false);
		engine.MoveLeft(false);
		twosec -= 2f;
	}
}

