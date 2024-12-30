using UnityEngine;

public class Flapper : MonoBehaviour {
	private bool upwards = false;
	private int noOfSteps = 3;
	private int step;

	private void Start() {
		step = noOfSteps;
	}

	void Update() {
		if (!upwards) {
			if (step-- > 0) {
				transform.Rotate(new Vector3(0f, 0f, -1000f) * Time.deltaTime);
			} else {
				upwards = true;
			}
		} else {
			if (step++ < noOfSteps) {
				transform.Rotate(new Vector3(0f, 0f, 1000f) * Time.deltaTime);
			} else {
				upwards = false;
			}
		}
	}
}
