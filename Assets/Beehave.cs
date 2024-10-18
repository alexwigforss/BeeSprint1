using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beehave : MonoBehaviour
{
	Rigidbody rb;
	AutoMove engine = new AutoMove();
	public Transform HiveLocation;
    
	// Start is called before the first frame update
	void Start()
    {
		engine = GetComponent<AutoMove>();
		rb = GetComponent<Rigidbody>();
		//rb.transform.position = HiveLocation.position;
		engine.ResetAll();
	}

	// Update is called once per frame
	void Update()
    {
		// engine.RotateAround();
		// engine.Ascend(true);
		//engine.RotateLeft(true);
		engine.Realign(true);
    }
}
