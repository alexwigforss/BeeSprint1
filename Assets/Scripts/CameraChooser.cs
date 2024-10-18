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
	public PlayerInput playerInput;

	Transform toptrans;
	Transform beetrans;

	bool manageMode;

	bool canSwitch = true;
	float switchCooldown = 0.5f; // Cooldown time in seconds

	void Start()
    {
		manageMode = false;
		Debug.Log("Camera choose started");
		toptrans = TopDownCamera.transform;
		beetrans = ThirdBeePerspectiveCamera.transform;

		if (playerInput == null)
		{
			playerInput = GetComponent<PlayerInput>();
		}
		playerInput.SwitchCurrentActionMap("InGameBeePov");

	}

	public void HandleTabPressed(InputAction.CallbackContext context)
	{
		if (context.started && canSwitch)
		{
			manageMode = !manageMode;
			ChangeCamera();
			StartCoroutine(SwitchCooldown());
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

			playerInput.SwitchCurrentActionMap("GuiButtons");
			return;
		}
		tdAudioListener.enabled=true;
		ThirdBeePerspectiveCamera.enabled = true;
		ThirdBeePerspectiveCamera.tag = "MainCamera";

		TopDownCamera.enabled = false;
		TopDownCamera.tag = "Untagged";
		tbAudioListener.enabled=false;

		playerInput.SwitchCurrentActionMap("InGameBeePov");
	}

	private IEnumerator SwitchCooldown()
	{
		canSwitch = false;
		yield return new WaitForSeconds(switchCooldown);
		canSwitch = true;
	}

	public void SwitchToActionMap(string actionMapName)
	{
		playerInput.SwitchCurrentActionMap(actionMapName);
	}
}
