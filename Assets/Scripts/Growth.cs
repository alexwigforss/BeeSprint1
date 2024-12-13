using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Growth : MonoBehaviour
{
	[SerializeReference]
	Transform petalTrans;

	[SerializeReference]
	SphereCollider OvaryColide;

	[SerializeReference]
	GameObject hitzone;

	[SerializeReference]
	public TMP_Text statsText;

	// Find the GameObject with the specified tag
	// GameObject targetObject = GameObject.FindWithTag("Spike");
	public string targetTag = "Spike"; // Set this to the tag of your target GameObject
	GameObject targetObject;
	Renderer renderer;
	private int phase = 0;
	private Vector3 scaleChange;
	private Vector3 petalsStartScale;
	private int fc;
	public float radius;
	public bool imortal = false;
	// Start is called before the first frame update
	private bool wilt = false;
	Material material;
	Color color = Color.black;
	public int spawnById = -1;

	void Start()
	{
		if (!imortal)
		{
			scaleChange = new Vector3(0.01f, 0.01f, 0.01f);
			petalsStartScale = new Vector3(0.1f, 0.1f, 0.1f);
			transform.localScale = scaleChange;
			petalTrans.localScale = scaleChange;

		}
		fc = 0;

		targetObject = FindChildWithTag(transform, targetTag);
		renderer = targetObject.GetComponent<Renderer>();
		material = renderer.material;
		hitzone.SetActive(false);
	}

	void Update()
	{
		if (phase == 0)
		{
			OvaryColide.enabled = false;
			if (transform.localScale.x < 1f)
			{
				transform.localScale += scaleChange * 5 * Time.deltaTime;
			}
			else
			{
				phase++;
			}
		}
		else if (phase == 1)
		{
			if (hitzone.activeSelf == false)
            {
	            hitzone.SetActive(true);
				Hitzones.HitPositions[spawnById].Add(hitzone.transform);
				statsText.text = Hitzones.PtrintHitListCount().ToString();
			}

			OvaryColide.enabled = true;
			if (petalTrans.localScale.x < 1f)
			{
				petalTrans.localScale += scaleChange * 2 * Time.deltaTime;
			}
			else
			{
				phase++;
			}
		}
        if (imortal)
        {
			return;
        }
        else if (phase == 2 && !wilt)
        {
            InitWilt();
            wilt = true;
        }

        else if (phase == 2)
        {
            OvaryColide.enabled = false;
            if (transform.localScale.x > 0f)
            {
                transform.localScale -= scaleChange * 5 * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

	private void InitWilt()
	{
		if (targetObject != null)
		{
			if (hitzone.activeSelf == true)
			{
				hitzone.SetActive(false);
				Hitzones.HitPositions[spawnById].Remove(hitzone.transform);
				statsText.text = Hitzones.PtrintHitListCount().ToString();
			}

			// Get the Renderer component from the target GameObject
			if (renderer != null)
			{
				// Modify the material properties
				material.SetFloat("_Mode", 2); // Set rendering mode to Fade
				material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				material.SetInt("_ZWrite", 0);
				material.DisableKeyword("_ALPHATEST_ON");
				material.EnableKeyword("_ALPHABLEND_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
				material.SetColor("_Color", color);
			}
			else
			{
				Debug.LogError("Renderer not found in the target GameObject.");
			}
		}
		else
		{
			Debug.LogError("Target GameObject with the specified tag not found.");
		}
	}

	GameObject FindChildWithTag(Transform parent, string tag)
	{
		foreach (Transform child in parent)
		{
			if (child.CompareTag(tag))
			{
				return child.gameObject;
			}
			GameObject result = FindChildWithTag(child, tag);
			if (result != null)
			{
				return result;
			}
		}
		return null;
	}
}
