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
	public int state = (int)States.search;
	float timer = 0f;
	float twosec = 0f;
	int turndirection = 0;

	public GameObject flowerSpawners;
	int selectedSpecie = 0;
	Queue<Transform> internalHitList = new Queue<Transform>();

	private enum States
	{
		home,
		idle,
		search,
		collect
	}

	void Start()
	{
		target = goal.transform;
		engine = GetComponent<AutoMove>();
		re = GetComponent<Realigner>();
		engine.ResetAll();
		postDist = Vector3.Distance(transform.position, goal.transform.position);
	}

	private void OnValidate()
	{
        if (Application.isPlaying)
        {
			getGoalQueue();
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
				break;
			case (int)States.collect:
				MoveTowards(target);
				break;
			default:
				Debug.Log("Not moving!");
				break;
		}
	}

	private void getGoalQueue()
	{
		Debug.Log("Getting Queue");
		internalHitList = Hitzones.hitList;
	}

	private void getNextGoal()
	{
		goal = internalHitList.Dequeue();

		Debug.Log("Getting Goal, hit list size = " + internalHitList.Count);
	}

	public void OnTriggerEnter(Collider other)
	{
		// TBD Implement return to home when fully loaded.
		//if (other == goalCollider)
		if (other.tag == "FlowerHitZone")
		{
            if (internalHitList.Count > 0)
            {
				// stashedGoal = goal;
				getNextGoal();
				//return;
			}
            else
            {
				Debug.Log("hitListWas ZERO");
	            stashedGoal = goal;
				goal = HiveLocation;
				// Debug.Log("Colliding with goalCollider!");
				//return;
            }
		}
		else if (other.CompareTag("Nest"))
		{
			Debug.Log("Hit nest");
			getGoalQueue();
			getNextGoal();
			//goal = stashedGoal;
		}
		target = goal.transform;

	}

	private void MoveTowards(Transform t)
	{
		prioDist = postDist;
		postDist = Vector3.Distance(transform.position, t.position);
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
		// Debug.Log("Change Direction");
		turndirection = Random.Range(0, 2);
		engine.MoveRight(false);
		engine.MoveLeft(false);
		twosec -= 2f;
	}

	private void LateUpdate()
	{

	}
}

