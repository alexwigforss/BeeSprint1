using UnityEngine;

public class LODCameraSetter : MonoBehaviour {
    private Transform mainCameraTransform;
    private LODGroup lodGroup;

    void Awake() {
        // Debug.Log("LODCameraSetter Awake");
    }

    void Start() {
        // Debug.Log("LODCameraSetter Start");
        Initialize();
    }

    public void Initialize() {
        mainCameraTransform = Camera.main?.transform;
        lodGroup = GetComponent<LODGroup>();
        if (lodGroup == null) {
            Debug.LogError("LODGroup component not found on this GameObject.");
        }
    }

    void Update() {
        if (Camera.main != null && mainCameraTransform != Camera.main.transform) {
            mainCameraTransform = Camera.main.transform;
        }

        if (lodGroup != null) {
            lodGroup.localReferencePoint = mainCameraTransform.position;
        } else {
            Debug.LogWarning("LODGroup is null.");
        }
    }
}

