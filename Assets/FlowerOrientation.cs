using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerOrientation : MonoBehaviour
{

	[SerializeField]
	Camera BeeCam;
	[SerializeField]
	Camera TopCam;
	void Start()
    {
        
    }

    void Update()
    {
		Vector3 targetPosition = Camera.main.transform.position;
		targetPosition.y = transform.position.y; // Preserve the plane's Y position
		transform.LookAt(targetPosition);
	}
}
