using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The CameraChooser class is responsible for managing switch between the top-down and third-person camera views.
/// </summary>
/// <remarks>
/// No audio in the game so far but the class takes audio listeners as references for later use.
public class CameraChooser : MonoBehaviour {
	[SerializeField]
	Camera TopDownCamera;
	[SerializeField]
	AudioListener tdAudioListener;
	[SerializeField]
	Camera ThirdBeePerspectiveCamera;
	[SerializeField]
	AudioListener tbAudioListener;
	public PlayerInput playerInput;
	bool manageMode;
	bool canSwitch = true;

	void Start() {
		manageMode = false;
		Debug.Log("Camera choose started");
		if (playerInput == null) {
			playerInput = GetComponent<PlayerInput>();
		}
		playerInput.SwitchCurrentActionMap("InGameBeePov");

	}

	/// <summary>
	/// TabPressed is called when the player presses the Tab key.
	/// </summary>
	/// <param name="context"></param>
	public void HandleTabPressed(InputAction.CallbackContext context) {
		if (context.started && canSwitch) {
			manageMode = !manageMode;
			ChangeCamera();
			StartCoroutine(SwitchCooldown());
		}
	}

	/// <summary>
	/// ChangeCamera switches between the top-down and third-person camera views.
	/// By enabling and disabling the cameras and audio listeners.
	/// </summary>
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
		tdAudioListener.enabled = true;
		ThirdBeePerspectiveCamera.enabled = true;
		ThirdBeePerspectiveCamera.tag = "MainCamera";

		TopDownCamera.enabled = false;
		TopDownCamera.tag = "Untagged";
		tbAudioListener.enabled = false;

		playerInput.SwitchCurrentActionMap("InGameBeePov");
	}

	/// <summary>
	///	Cooldown for switching between the top-down and third-person camera views.
	///	</summary>
	///	<remarks>
	///	Not longer needed in this context but kept for future reference.
	///	</remarks>
	private IEnumerator SwitchCooldown() {
		canSwitch = false;
		yield return new WaitForSeconds(0.0f);
		canSwitch = true;
	}

	/// <summary>
	/// Switches the current action map of the player input because the both views have different control schemes.
	/// </summary>
	public void SwitchToActionMap(string actionMapName) {
		playerInput.SwitchCurrentActionMap(actionMapName);
	}
}
