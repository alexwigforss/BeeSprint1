using UnityEngine;

public class Flapper : MonoBehaviour {
	private bool upwards = false;
	private int noOfSteps = 3;
	private int step;
	private Transform mainCamera;
	[SerializeField] private float lodDistance = 20.0f;

	private void Start() {
		step = noOfSteps;
		mainCamera = Camera.main.transform;
	}

	void Update() {
		if (Vector3.Distance(transform.position, mainCamera.position) > lodDistance) {
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
