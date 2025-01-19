using UnityEngine;

/// <summary>
/// The BeeSelection class is responsible for managing the selection sphere of a NPC bee in the game.
/// The selection sphere is a transparent sphere used to indicate the selected bees in the game.
/// </summary>
public class BeeSelection : MonoBehaviour {
	[SerializeField] GameObject ssphere;

	public void EnableSphere() {
		if (ssphere != null) {
			ssphere.SetActive(true);
		} else {
			Debug.LogWarning("ssphere is not assigned in the Inspector.");
		}
	}

	public void DisableSphere() {
		if (ssphere != null) {
			ssphere.SetActive(false);
		} else {
			Debug.LogWarning("ssphere is not assigned in the Inspector.");
		}
	}
}

