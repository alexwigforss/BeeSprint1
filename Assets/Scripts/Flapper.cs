using UnityEngine;

public class Flapper : MonoBehaviour {
	private bool upwards = false;
	private int noOfSteps = 3;
	private int step;
	private Transform mainCameraTransform;
	[SerializeField] private float lodDistance = 20.0f;

	private void Start() {
		step = noOfSteps;
		mainCameraTransform = Camera.main.transform;
	}

	void Update() {
		// Check if the main camera has changed
		if (Camera.main != null && mainCameraTransform != Camera.main.transform) {
			mainCameraTransform = Camera.main.transform;
		}

		if (mainCameraTransform == null) {
			return; // No main camera found
		}

		if (Vector3.Distance(transform.position, mainCameraTransform.position) > lodDistance) {
			return;
		}

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
