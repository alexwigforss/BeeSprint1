using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
	[SerializeReference]
	Transform petalTrans;
	[SerializeReference]
	Transform lockAtCam;
	[SerializeReference]
	SphereCollider OvaryColide;

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
	// Start is called before the first frame update
	private bool wilt = false;
	Material material;
	Color color = Color.black;

	void Start()
	{
		scaleChange = new Vector3(0.01f, 0.01f, 0.01f);
		petalsStartScale = new Vector3(0.1f, 0.1f, 0.1f);
		transform.localScale = scaleChange;
		petalTrans.localScale = scaleChange;
		fc = 0;

		targetObject = FindChildWithTag(transform, targetTag);
		renderer = targetObject.GetComponent<Renderer>();
		material = renderer.material;
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 targetPosition = Camera.main.transform.position;
		targetPosition.y = transform.position.y; // Preserve the plane's Y position
		transform.LookAt(targetPosition); if (phase == 0)
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
