using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Beehave class is responsible for managing the behavior of a NPC bee in the game.
/// It controls the bee's state transitions, movement, and interactions with other game objects.
/// </summary>
/// 
/// <remarks>
/// The class uses several components to achieve its functionality:
/// - AutoMove: Handles the movement of the bee.
/// - Realigner: Aligns the bee's position and orientation.
/// - Collision: Manages collision detection and response.
/// - SelectiveMemory: Stores and manages the bee's memory of collectible items.
/// 
/// The class also includes methods for:
/// - Moving towards a target.
/// - Randomly changing direction.
/// - Getting the nearest sibling bee.
/// - Managing the bee's memory (goal lists).
/// - Handling collision with resources and nest.
/// </remarks>

public class Beehave : MonoBehaviour {
	Transform goal;
	AutoMove engine;
	Realigner re;
	public Transform HiveLocation;
	Transform target;
	private float postDist;
	bool aligned = false;
	public int state;
	float timer = 0f;
	float twosec = 0f;
	int turndirection = 0;
	int goalItterator = 0;
	List<Transform> internalHitList;

	Collision collision;

	private void Awake() {
		internalHitList = new List<Transform>();
	}

	/// <summary>
	/// The bee's behavior is governed by a finite state machine with the following states:
	/// - home: The bee stays at its hive.
	/// - idle: The bee is idle (flying in circle) until hitzone positions are in memory.
	/// - search: The bee is flying around taking random turns until hitzone positions are in memory.
	/// - collect: The bee collects resources from targets in memory.
	/// - water: The bee collects water. (Not Implemented)
	/// - propol: The bee collects propolis. (Not Implemented)
	/// - unload: The bee returns to the hive to unload collected resources.
	/// <summary>
	private enum States {
		home,
		idle,
		search,
		collect,
		water,
		propol,
		unload
	}

	void Start() {
		engine = GetComponent<AutoMove>();
		re = GetComponent<Realigner>();
		collision = GetComponent<Collision>();
		engine.ResetAll();
		state = (int)States.idle;
	}

	void Update() {
		timer += Time.deltaTime;
		twosec += Time.deltaTime;
		StatesOfBeehave();

		/// <summary>
		/// a method to check if there are any targets to collect resources from
		/// and set state to collect if so.
		/// </summary>
		void IfTargetsStartCollect() {
			GetGoalLists();
			if (internalHitList.Count > 0) {
				GetNextRandomGoal();
				target = goal;
				state = (int)States.collect;
			}
		}

		/// <summary>
		/// a method to check the current state of the bee and perform the appropriate actions.
		/// </summary>
		void StatesOfBeehave() {
			switch (state) {
				case (int)States.home:
					aligned = false;
					transform.position = HiveLocation.transform.position;
					break;
				case (int)States.idle:
					if (!aligned) { aligned = re.AlignXZ(true); }
					if (timer >= 0.8f) { engine.MoveForward(true); } else { engine.MoveForward(false); }
					engine.MoveRight(true, 2);
					IfTargetsStartCollect();
					break;
				case (int)States.search:
					if (!aligned) { aligned = re.AlignXZ(true); }
					engine.MoveForward(true);
					if (turndirection == 0) { engine.MoveRight(true); } else { engine.MoveLeft(true); }
					if (twosec >= 2f) { RandomDirection(); }
					IfTargetsStartCollect();
					break;
				case (int)States.collect:
					if (target != null && target.gameObject.activeInHierarchy) {
						MoveTowards(target);
					} else {
						GetGoalLists();
						GetNextRandomGoal();
						target = goal;
					}
					if (collision.totalLoad >= collision.maxload) {
						target = HiveLocation;
						state = (int)States.unload;
					}
					break;
				case (int)States.unload:
					if (collision.totalLoad > 0) {
						target = HiveLocation;
						MoveTowards(target);
					} else {
						GetGoalLists();
						GetNextRandomGoal();
						target = goal;
						if (internalHitList.Count <= 0) {
							state = (int)States.idle;
						} else {
							state = (int)States.collect;
						}
					}
					break;
				default:
					Debug.Log("Not moving!");
					break;
			}
		}
	}

	/*
	public void GetTargetFromSibling() {
		Transform parent = transform.parent;
		if (parent == null) {
			Debug.LogWarning("No parent found.");
			return;
		}
		float minDistance = float.MaxValue;
		Transform nearestSibling = null;
		foreach (Transform sibling in parent) {
			if (sibling != transform) {
				float distance = Vector3.Distance(transform.position, sibling.position);
				if (distance < minDistance) {
					minDistance = distance;
					nearestSibling = sibling;
				}
			}
		}
		if (nearestSibling != null) {
			target = nearestSibling;
			Debug.Log("Nearest sibling found: " + target.name);
		} else {
			Debug.LogWarning("No siblings found.");
		}
	}
	*/

	/// <summary>
	/// Accesing the SelectiveMemory component from the parent bee group object.
	/// </summary>
	/// <returns></returns>
	SelectiveMemory GetBeeMemory() {
		Transform trans = transform.parent;
		GameObject go = trans.gameObject;

		if (go.TryGetComponent<SelectiveMemory>(out var memory)) {
			return memory;
		} else {
			//Debug.Log("SelectiveMemory not found");
			return null;
		}
	}

	/// <summary>
	/// Getting a portion of the hitzone list from the parent bee group object.
	/// </summary>
	private void GetGoalLists() {
		//GetBeeMemory().PrintSpecies();
		try {
			internalHitList.Clear();
			foreach (var i in GetBeeMemory().GetSpecies()) {
				List<Transform> tempList = Hitzones.HitPositions[i];
				if (tempList.Count > 4) {
					tempList.RemoveRange(0, tempList.Count / 2);
				}
				internalHitList.AddRange(tempList);
			}
			// Debug.Log("Got " + internalHitList.Count + " hitzonez");
		} catch (System.Exception e) {
			Debug.LogException(e.InnerException);
		}
	}

	/// <summary>
	/// Getting the next goal from the hitzone list.
	/// </summary>
	/// <remarks>
	/// unused at the moment but may come in handy later if more deterministic behavior is needed.
	private void GetNextGoal() {
		if (internalHitList.Count > 0) {
			if (goalItterator < internalHitList.Count - 1) {
				goalItterator++;
			} else {
				goalItterator = 0;
			}
			goal = internalHitList[goalItterator];
			//Debug.Log("Getting Goal [" + goalItterator + "], from hit list size = " + internalHitList.Count);
		}
	}

	/// <summary>
	/// Getting a random goal from the hitzone list.
	/// </summary>
	private void GetNextRandomGoal() {
		if (internalHitList.Count > 0) {
			int randomgoal = Random.Range(0, internalHitList.Count);
			goal = internalHitList[randomgoal];
			//Debug.Log("Getting Goal [" + goalItterator + "], from hit list size = " + internalHitList.Count);
		}
	}

	public void OnTriggerEnter(Collider other) {
		if (goal == null) {
			return;
		}
		if (other.tag == "FlowerHitZone") {
			if (internalHitList.Count > 0) {
				GetNextRandomGoal();
			} else {
				goal = HiveLocation;
			}
		} else if (other.CompareTag("Nest")) {
			GetGoalLists();
			GetNextRandomGoal();
		}
		if (goal != null) {
			target = goal.transform;
		}
	}

	/// <summary>
	/// Move the bee towards a target goal.
	/// if close it turns a little extra to avoid orbiting.
	/// If flower dies it will try to reach the next goal.
	/// </summary>
	private void MoveTowards(Transform t) {
		postDist = Vector3.Distance(transform.position, t.position);
		engine.RotateTowards(t.position);
		engine.MoveForward(true);
		// HACK To avoid eternal orbiting around small targets we perform one extra rotation
		engine.RotateTowards(t.position);
		if (postDist < 0.9) {
			engine.RotateTowards(t.position * 8);
			// Check if the zone is still in list
			// To avoid chasing dead targets
			if (!Hitzones.Contain(t)) {
				// Debug.LogWarning("Trying to reach dead hitzone");
				GetGoalLists();
				GetNextRandomGoal();
			}
		}
	}

	/// <summary>
	/// Randomly change direction every 2 seconds.
	/// </summary>
	private void RandomDirection() {
		// Debug.Log("Change Direction");
		turndirection = Random.Range(0, 2);
		engine.MoveRight(false);
		engine.MoveLeft(false);
		twosec -= 2f;
	}
}

