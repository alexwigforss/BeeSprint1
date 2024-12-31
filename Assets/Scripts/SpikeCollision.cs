using UnityEngine;

public class SpikeCollision : MonoBehaviour {
	private FlowerSpawner parentScript;

	void Start() {
		parentScript = GetComponentInParent<FlowerSpawner>();
		if (parentScript == null) {
			Debug.LogError("flowerSpawner component not found in parent hierarchy.");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (parentScript != null) {
			// Notify the parent about the trigger event
			parentScript.OnChildTrigger();
		} else {
			Debug.LogError("Cannot call OnChildTrigger because parentScript is null.");
		}
	}
}
