using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.GraphicsBuffer;

public class Beehave : MonoBehaviour
{
	[SerializeField]
	Transform goal;
	//Transform stashedGoal;
	//[SerializeField]
	//Collider goalCollider;

	AutoMove engine;
	Realigner re;
	public Transform HiveLocation;
	Transform target;
	//private float prioDist;
	private float postDist;

	bool aligned = false;
	public int state;
	float timer = 0f;
	float twosec = 0f;
	int turndirection = 0;


	public int selectedSpecie = 0;
	int goalItterator = 0;
	List<Transform> internalHitList;

	Collision collision;

	private void Awake()
	{
		internalHitList = new List<Transform>();
	}

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
		engine = GetComponent<AutoMove>();
		re = GetComponent<Realigner>();
		collision = GetComponent<Collision>();
		engine.ResetAll();
		state = (int)States.idle;
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
				IfTargetsStartCollect();
				break;
			case (int)States.search:
				if (!aligned) { aligned = re.AlignXZ(true); }
				engine.MoveForward(true);
				if (turndirection == 0) { engine.MoveRight(true); }
				else { engine.MoveLeft(true); }
				if (twosec >= 2f) { RandomDirection(); }
				IfTargetsStartCollect();
				break;
			case (int)States.collect:
				if (target != null && target.gameObject.activeInHierarchy)
				{
					MoveTowards(target);
				}
				else
				{
					getGoalLists();
					getNextGoal();
					target = goal;
				}
				if (collision.totalLoad >= collision.maxload)
				{
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
					getGoalLists();
					getNextGoal();
					target = goal;
					if (internalHitList.Count <= 0)
					{
						state = (int)States.idle;
					}
					else
					{
						state = (int)States.collect;
					}
				}
				break;
			default:
				Debug.Log("Not moving!");
				break;
		}

		void IfTargetsStartCollect()
		{
			getGoalLists();
			if (internalHitList.Count > 0)
			{
				getNextGoal();
				target = goal;
				state = (int)States.collect;
			}
		}
	}

	public void GetTargetFromSibling()
	{
		Transform parent = transform.parent;
		if (parent == null)
		{
			Debug.LogWarning("No parent found.");
			return;
		}

		float minDistance = float.MaxValue;
		Transform nearestSibling = null;

		foreach (Transform sibling in parent)
		{
			if (sibling != transform)
			{
				float distance = Vector3.Distance(transform.position, sibling.position);
				if (distance < minDistance)
				{
					minDistance = distance;
					nearestSibling = sibling;
				}
			}
		}

		if (nearestSibling != null)
		{
			target = nearestSibling;
			Debug.Log("Nearest sibling found: " + target.name);
		}
		else
		{
			Debug.LogWarning("No siblings found.");
		}
	}

	SelectiveMemory GetBeeMemory()
	{
		Transform trans = transform.parent;
		GameObject go = trans.gameObject;

		if (go.TryGetComponent<SelectiveMemory>(out var memory))
		{
			return memory;
		}
		else
		{
			//Debug.Log("SelectiveMemory not found");
			return null;
		}
	}

	private void getGoalLists()
	{
		//GetBeeMemory().PrintSpecies();
		try
		{
			internalHitList.Clear();
			foreach (var i in GetBeeMemory().GetSpecies())
			{
				List<Transform> tempList = Hitzones.HitPositions[i];
				if (tempList.Count > 4)
				{
					tempList.RemoveRange(0, tempList.Count / 2);
				}
				internalHitList.AddRange(tempList);

			}
			// Debug.Log("Got " + internalHitList.Count + " hitzonez");
		}
		catch (System.Exception e)
		{
			Debug.LogException(e.InnerException);
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
			//Debug.Log("Getting Goal [" + goalItterator + "], from hit list size = " + internalHitList.Count);
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (goal == null)
		{
			return;
		}
		if (other.tag == "FlowerHitZone")
		{
			if (internalHitList.Count > 0)
			{
				getNextGoal();
			}
			else
			{
				goal = HiveLocation;
			}
		}
		else if (other.CompareTag("Nest"))
		{
			getGoalLists();
			getNextGoal();
		}
			target = goal.transform;
	}

	private void MoveTowards(Transform t)
	{
		postDist = Vector3.Distance(transform.position, t.position);
		engine.rotateTowards(t.position);
		engine.MoveForward(true);
		engine.rotateTowards(t.position);
		if (postDist < 0.9)
		{
			engine.rotateTowards(t.position * 8);
			// Check if the zone is still in list
			// To avoid chasing dead targets
			if (!Hitzones.Contain(t))
			{
				// Debug.Log("Trying to reach dead hitzone");
				getGoalLists();
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

