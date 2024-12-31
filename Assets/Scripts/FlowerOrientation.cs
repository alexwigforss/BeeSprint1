using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlowerOrientation : MonoBehaviour {

	[SerializeField]
	Camera BeeCam;
	[SerializeField]
	Camera TopCam;

	void Update() {
		try {
			Vector3 targetPosition = Camera.main.transform.position;
			targetPosition.y = transform.position.y; // Preserve the plane's Y position
			transform.LookAt(targetPosition);
		} catch (System.Exception) {
			//throw;
		}
	}
}
