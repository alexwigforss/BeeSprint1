using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Flapper : MonoBehaviour
{
	double flappFromRotation = -0.15f;
	double flappToRotation = -0.5f;
	private bool upwards = false;
	private int noOfSteps = 3;
	private int step;

	private void Start()
	{
		step = noOfSteps;
	}
	void Update()
	{
		if (!upwards)
		{
			if (step-- > 0)
			{
				transform.Rotate(new Vector3(0f, 0f, -1000f) * Time.deltaTime);
			}
			else
			{
				upwards = true;
			}
		}
		else
		{
			if (step++ < noOfSteps)
			{
				transform.Rotate(new Vector3(0f, 0f, 1000f) * Time.deltaTime);
			}
			else
			{
				upwards = false;
			}
		}
	}
}
