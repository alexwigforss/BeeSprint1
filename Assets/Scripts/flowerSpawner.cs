using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class flowerSpawner : MonoBehaviour
{
	// public List<Texture2D> textureList; // Assign your textures in the Inspector
	public Texture2D texture; // Assign your textures in the Inspector
	public GameObject flower;
	public int childNoOfSeeds = 5;
	//public GameObject targetObject;
    int randNum;
    public Transform motherDest;
    private Transform spawnDest;
    public bool spawningbool = true;
    public float spawnTime;
	public float betweenSpawnTimeMillis = 1000;
	float windX, windZ;
	float globalradius = 1.0f;
	public float minSpawndDist = 1.0f;
	public float maxSpawndDist = 2.0f;
	public string targetTag = "Spike"; // Set this to the tag of your target GameObject
	static int enumerator = 0;
	public int ID = 0;

	[SerializeReference]
	public TMP_Text statsText;
	void Start()
	{
		ID = getIdByName();
		
		Debug.Log("Flower Spaner id: " + ID + "has entered the scene");
		spawnDest = motherDest;
		windX = 0;
		windZ = 0;
		globalradius = UnityEngine.Random.Range(0.5f, 2f);
		StartCoroutine(Spawning());
	}

	private int getIdByName()
	{
		{
			string parentName = transform.parent.name; int r = 0;
			// Use regular expression to find the number within parentheses
			Match match = Regex.Match(parentName, @"\((\d+)\)");
			if (match.Success) { r = int.Parse(match.Groups[1].Value); }
			Debug.Log("Parent name is: " + parentName + " My number is: " + r);
			return r;
		}
	}
	void Update()
	{
		//Debug.Log(Time.frameCount);
		if (Time.frameCount % betweenSpawnTimeMillis == 0 && childNoOfSeeds > 0)
		{
			spawningbool = true;
			StartCoroutine(Spawning());
			childNoOfSeeds--;
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

	IEnumerator Spawning()
	{
		while (spawningbool)
		{
			yield return new WaitForSeconds(spawnTime);
			randNum = 0;
			// Debug.Log("BOOM Spawntime = " + spawnTime);
			// Calculate a new random position for each spawn
			float windX, windZ;
			GetRandomInCircle(out windX, out windZ);
			Vector3 spawnPosition = new Vector3(spawnDest.position.x + windX, spawnDest.position.y, spawnDest.position.z + windZ);

			GameObject spawnedObject = Instantiate(flower, spawnPosition, spawnDest.rotation);
			spawnedObject.transform.SetParent(transform);
			spawnedObject.GetComponent<Growth>().radius = globalradius;
			spawnedObject.GetComponent<Growth>().statsText = statsText;
			spawnedObject.GetComponent<Growth>().spawnById = ID;
			

			// Find the target GameObject by tag
			GameObject targetObject = FindChildWithTag(spawnedObject.transform, targetTag);

			if (targetObject != null)
			{
				// Find the mesh renderer in the target GameObject
				MeshRenderer meshRenderer = targetObject.GetComponent<MeshRenderer>();

				if (meshRenderer != null)
				{
					ApplyMaterial(meshRenderer);
				}
				else
				{
					Debug.LogError("MeshRenderer not found in the target GameObject.");
				}
			}
			else
			{
				Debug.LogError("Target GameObject with the specified tag not found.");
			}

			spawningbool = false;
		}
	}

	private void ApplyMaterial(MeshRenderer meshRenderer)
	{
		// Create a new material with the texture
		Material newMaterial = new Material(Shader.Find("Standard"));
		newMaterial.mainTexture = texture;

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

	void GetRandomInCircle(out float x, out float y)
	{
		float angle = UnityEngine.Random.Range(0.0f, (float)(Math.PI*2));
		float r = UnityEngine.Random.Range(minSpawndDist, maxSpawndDist * 2);
		x = r * Mathf.Cos(angle);
		y = r * Mathf.Sin(angle);
	}
	public void OnChildTrigger() 
	{
		childNoOfSeeds++;
		// Debug.Log("KABOOM RECIEVED" + childNoOfSeeds);
	}
}
