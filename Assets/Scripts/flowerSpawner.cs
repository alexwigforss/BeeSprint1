using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour {
	public Texture2D texture;
	public GameObject flower;
	public int childNoOfSeeds = 5;
	public Transform motherDest;
	private Transform spawnDest;
	public bool spawningbool = true;
	public float spawnTime;
	public float betweenSpawnTimeMillis = 1000;
	float globalradius = 1.0f;
	public float minSpawndDist = 1.0f;
	public float maxSpawndDist = 2.0f;
	public string targetTag = "Spike";
	[HideInInspector]
	public int ID = 0;

	[SerializeReference]
	public TMP_Text statsText;

	/// <summary>
	/// Initializes the FlowerSpawner, sets the ID, and starts the spawning coroutine.
	/// </summary>
	void Start() {
		ID = GetIdByName();
		Hitzones.HitPositions[ID] = new List<Transform> { };
		spawnDest = motherDest;
		globalradius = UnityEngine.Random.Range(0.5f, 2f);
		StartCoroutine(Spawning());
	}

	/// <summary>
	/// Gets the ID of the FlowerSpawner based on its parent's name.
	/// </summary>
	/// <returns>The ID extracted from the parent's name.</returns>
	private int GetIdByName() {
		string parentName = transform.parent.name;
		int r = 0;
		// Use regular expression to find the number within parentheses
		Match match = Regex.Match(parentName, @"\((\d+)\)");
		if (match.Success) { r = int.Parse(match.Groups[1].Value); }
		Debug.Log("Parent name is: " + parentName + " My number is: " + r);
		return r;
	}

	/// <summary>
	/// Updates the FlowerSpawner, periodically starting the spawning coroutine.
	/// </summary>
	void Update() {
		if (Time.frameCount % betweenSpawnTimeMillis == 0 && childNoOfSeeds > 0) {
			spawningbool = true;
			StartCoroutine(Spawning());
			childNoOfSeeds--;
		}
	}

	/// <summary>
	/// Finds a child GameObject with the specified tag.
	/// </summary>
	/// <param name="parent">The parent Transform to search within.</param>
	/// <param name="tag">The tag to search for.</param>
	/// <returns>The child GameObject with the specified tag, or null if not found.</returns>
	GameObject FindChildWithTag(Transform parent, string tag) {
		foreach (Transform child in parent) {
			if (child.CompareTag(tag)) {
				return child.gameObject;
			}
			GameObject result = FindChildWithTag(child, tag);
			if (result != null) {
				return result;
			}
		}
		return null;
	}

	/// <summary>
	/// Coroutine for spawning flowers at random positions within a specified radius.
	/// </summary>
	/// <returns>An IEnumerator for the coroutine.</returns>
	IEnumerator Spawning() {
		while (spawningbool) {
			yield return new WaitForSeconds(spawnTime);
			GetRandomInCircle(out float windX, out float windZ);
			Vector3 spawnPosition = new(spawnDest.position.x + windX, spawnDest.position.y, spawnDest.position.z + windZ);
			GameObject spawnedObject = Instantiate(flower, spawnPosition, spawnDest.rotation);
			spawnedObject.transform.SetParent(transform);
			spawnedObject.GetComponent<Growth>().radius = globalradius;
			spawnedObject.GetComponent<Growth>().statsText = statsText;
			spawnedObject.GetComponent<Growth>().spawnById = ID;
			GameObject targetObject = FindChildWithTag(spawnedObject.transform, targetTag);

			if (targetObject != null) {
				// Find the mesh renderer in the target GameObject
				MeshRenderer meshRenderer = targetObject.GetComponent<MeshRenderer>();
				if (meshRenderer != null) {
					ApplyMaterial(meshRenderer);
				} else {
					Debug.LogError("MeshRenderer not found in the target GameObject.");
				}
			} else {
				Debug.LogError("Target GameObject with the specified tag not found.");
			}
			spawningbool = false;
		}
	}

	/// <summary>
	/// Applies a material with the specified texture to the given MeshRenderer.
	/// </summary>
	/// <param name="meshRenderer">The MeshRenderer to apply the material to.</param>
	private void ApplyMaterial(MeshRenderer meshRenderer) {
		// Create a new material with the texture
		Material newMaterial = new(Shader.Find("Standard")) {
			mainTexture = texture
		};

		// Set rendering mode to Cutout
		newMaterial.SetFloat("_Mode", 1); // 1 corresponds to Cutout mode
		newMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
		newMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
		newMaterial.SetInt("_ZWrite", 1);
		newMaterial.DisableKeyword("_ALPHABLEND_ON");
		newMaterial.EnableKeyword("_ALPHATEST_ON");
		newMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		newMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;

		// Set alpha cutoff value
		newMaterial.SetFloat("_Cutoff", 0.77f);

		// Apply the new material to the mesh renderer
		meshRenderer.material = newMaterial;
	}

	/// <summary>
	/// Generates random coordinates within a circle.
	/// </summary>
	/// <param name="x">The x-coordinate of the random point.</param>
	/// <param name="y">The y-coordinate of the random point.</param>
	void GetRandomInCircle(out float x, out float y) {
		float angle = UnityEngine.Random.Range(0.0f, (float)(Math.PI * 2));
		float r = UnityEngine.Random.Range(minSpawndDist, maxSpawndDist * 2);
		x = r * Mathf.Cos(angle);
		y = r * Mathf.Sin(angle);
	}

	/// <summary>
	/// Increases the number of seeds when a child object triggers an event.
	/// </summary>
	public void OnChildTrigger() {
		childNoOfSeeds++;
	}
}
