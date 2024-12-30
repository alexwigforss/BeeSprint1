using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;

public class Resources : MonoBehaviour {
	public static int resources;
	public static int pol, pro, nec, wat;
	int prepol, prepro, prenec, prewat;

	[SerializeReference]
	public TMP_Text resourceText;
	// Start is called before the first frame update
	void Start() {
		resources = 0;
		pol = 0; pro = 0; nec = 0; wat = 0;
		prepol = 0; prepro = 0; prenec = 0; prewat = 0;
	}

	// Update is called once per frame
	void Update() {
		if (pol != prepol) {
			prepol = pol;
			UpdateResourceText();
			return;
		}
		if (pro != prepro) {
			prepro = pro;
			UpdateResourceText();
			return;
		}
		if (nec != prenec) {
			prenec = nec;
			UpdateResourceText();
			return;
		}
		if (wat != prewat) {
			prewat = wat;
			UpdateResourceText();
			return;
		}
	}

	private void UpdateResourceText() {
		if (resourceText != null) {
			// resourceText.text = "" + resources;
			resourceText.text = "Po:" + pol + " Ne:" + nec + " Pr:" + pro + " Wa:" + wat;
		} else {
			Debug.LogError("TMP_Text component not found!");
		}
	}
}
