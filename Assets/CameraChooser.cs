using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class CameraChooser : MonoBehaviour
{
    [SerializeField]
    Camera TopDownCamera;
	[SerializeField]
	AudioListener tdAudioListener;
	[SerializeField]
	Camera ThirdBeePerspectiveCamera;
	[SerializeField]
	AudioListener tbAudioListener;

	Transform toptrans;
	Transform beetrans;

	bool manageMode;

	// Start is called before the first frame update
	void Start()
    {
		manageMode = false;
		Debug.Log("Camera choose started");
		toptrans = TopDownCamera.transform;
		beetrans = ThirdBeePerspectiveCamera.transform;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	public void HandleTabPressed(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			manageMode = !manageMode;
			ChangeCamera();
		}
	}

	private void ChangeCamera() {
		if (manageMode) {
			tbAudioListener.enabled = false;
			TopDownCamera.enabled = true;
			TopDownCamera.tag = "MainCamera";

			ThirdBeePerspectiveCamera.enabled = false;
			ThirdBeePerspectiveCamera.tag = "Untagged";
			tdAudioListener.enabled = true;
			return;
		}
		tdAudioListener.enabled=true;
		ThirdBeePerspectiveCamera.enabled = true;
		ThirdBeePerspectiveCamera.tag = "MainCamera";

		TopDownCamera.enabled = false;
		TopDownCamera.tag = "Untagged";
		tbAudioListener.enabled=false;
	}

}
