using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml;
using System;

public class Collision : MonoBehaviour {
	public bool isPlayer = false;
	System.Random random = new System.Random();
	int pollen = 0;
	int nectar = 0;
	int propolis = 0;
	int water = 0;
	public int totalLoad = 0;
	public int maxload = 40;
	float loadspeed = 0.1f;
	private TMP_Text tmpText;

	bool hasLeftWater = false;
	bool hasLeftTree = false;
	bool hasLeftNest = false;
	private Coroutine decreaseCoroutine;
	private Coroutine increaseCoroutine;
	private Coroutine incPropCoroutine;

	//void OnValidate() {
	//	if (isPlayer) {
	//		if (Application.isPlaying) {
	//			try {
	//				tmpText = GetComponentInChildren<Canvas>().GetComponentInChildren<TMP_Text>();
	//				Debug.LogError("Found bee text");
	//			} catch (Exception) {
	//				Debug.LogError("No text on this bee");
	//			}
	//		}
	//	}
	//}

	void Start() {
		if (isPlayer) {
			StartCoroutine(InitializeText());
		}
	}

	IEnumerator InitializeText() {
		yield return null; // Wait for the next frame
		try {
			tmpText = GetComponentInChildren<Canvas>().GetComponentInChildren<TMP_Text>();
			Debug.LogError("Found bee text");
		} catch (Exception) {
			Debug.LogError("No text on this bee");
		}
	}


	// Update is called once per frame
	void Update() {

	}
	public void OnCollisionEnter(UnityEngine.Collision collision) {
		if (collision.gameObject.name.Equals("World")) {
			transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Flapper>().enabled = false;
			transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Flapper>().enabled = false;
		} else if (collision.gameObject.name.Equals("Water")) {
			hasLeftWater = false;
			increaseCoroutine ??= StartCoroutine(CollectWaterOverTime());
		} else if (collision.gameObject.name.StartsWith("Tree")) {
			hasLeftTree = false;
			incPropCoroutine ??= StartCoroutine(CollectPropolisOverTime());
		}

	}

	public void OnCollisionExit(UnityEngine.Collision collision) {
		if (collision.gameObject.name.Equals("World")) {
			transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Flapper>().enabled = true;
			transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Flapper>().enabled = true;
		} else if (collision.gameObject.name.Equals("Water")) {
			hasLeftWater = true;
			if (increaseCoroutine != null) {
				StopCoroutine(increaseCoroutine);
				increaseCoroutine = null;
			}
		} else if (collision.gameObject.name.StartsWith("Tree")) {
			hasLeftTree = true;
			if (incPropCoroutine != null) {
				StopCoroutine(incPropCoroutine);
				incPropCoroutine = null;
			}
		}

	}

	public void OnTriggerEnter(Collider other) {
		if (other.CompareTag("FlowerHitZone")) {
			if (totalLoad < maxload) {
				if (random.Next(0, 2) == 0) { pollen++; } else { nectar++; }
			}
			calcTotalLoad();
			UpdatePlayerText();
			//Debug.Log("Drone collected pollen " + pollen);
		} else if (other.CompareTag("Nest")) {
			hasLeftNest = false;
			decreaseCoroutine ??= StartCoroutine(UnloadResources());
		}
	}

	private void calcTotalLoad() {
		totalLoad = pollen + nectar + propolis + water;
	}

	public void OnTriggerExit(Collider other) {
		if (other.CompareTag("Nest")) {
			hasLeftNest = true;
			if (decreaseCoroutine != null) {
				StopCoroutine(decreaseCoroutine);
				decreaseCoroutine = null;
			}
		}
	}

	private void UpdatePlayerText() {
		if (isPlayer) {
			if (tmpText != null) {
				tmpText.text = "" + pollen + "," + nectar + "," + propolis + "," + water;
				if (totalLoad >= maxload) {
					//Debug.Log("Bamm Full");
					tmpText.text += "\nMaxLoad";
				}
			}
		}
	}

	IEnumerator CollectWaterOverTime() {
		//Debug.Log("Increasing" + water);
		while (totalLoad < maxload) {
			if (hasLeftWater) {
				hasLeftWater = false;
				break;
			}
			water++;
			calcTotalLoad();
			UpdatePlayerText();
			//Debug.Log("Post Increasing" + water);
			yield return new WaitForSeconds(loadspeed);
		}
		increaseCoroutine = null;
	}

	IEnumerator CollectPropolisOverTime() {
		//Debug.Log("Increasing" + water);
		while (totalLoad < maxload) {
			if (hasLeftTree) {
				hasLeftTree = false;
				break;
			}
			propolis++;
			calcTotalLoad();
			UpdatePlayerText();
			//Debug.Log("Post Increasing" + water);
			yield return new WaitForSeconds(loadspeed);
		}
		increaseCoroutine = null;
	}

	IEnumerator UnloadResources() {
		while (pollen > 0 || nectar > 0 || propolis > 0 || water > 0) {
			if (hasLeftNest) {
				hasLeftNest = false;
				break;
			}
			if (pollen > 0) {
				pollen--;
				Resources.pol++;
			}
			if (nectar > 0) {
				nectar--;
				Resources.nec++;
			}
			if (propolis > 0) {
				propolis--;
				Resources.pro++;
			}
			if (water > 0) {
				water--;
				Resources.wat++;
			}
			calcTotalLoad();
			UpdatePlayerText();
			yield return new WaitForSeconds(loadspeed);
		}
		decreaseCoroutine = null;
	}


}