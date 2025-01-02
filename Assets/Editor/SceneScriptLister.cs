using UnityEngine;
using UnityEditor;
using System.Text;

public class SceneScriptLister : EditorWindow {
	[MenuItem("Tools/List Scripts in Scene")]
	public static void ShowWindow() {
		GetWindow<SceneScriptLister>("List Scripts in Scene");
	}

	private void OnGUI() {
		if (GUILayout.Button("Generate Script List")) {
			GenerateScriptList();
		}
	}

	private void GenerateScriptList() {
		StringBuilder sb = new StringBuilder();
		GameObject[] allObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

		foreach (GameObject go in allObjects) {
			ListScripts(go, sb, 0);
		}

		string path = "Assets/SceneScriptList.txt";
		System.IO.File.WriteAllText(path, sb.ToString());
		AssetDatabase.Refresh();
		Debug.Log("Script list generated at " + path);
	}

	private void ListScripts(GameObject go, StringBuilder sb, int indentLevel) {
		string indent = new string(' ', indentLevel * 2);
		sb.Append(indent + go.name);

		MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour>();
		if (scripts.Length > 0) {
			sb.Append(" [");
			for (int i = 0; i < scripts.Length; i++) {
				if (scripts[i] != null) {
					sb.Append(scripts[i].GetType().Name);
					if (i < scripts.Length - 1) {
						sb.Append("] [");
					}
				}
			}
			sb.Append("]");
		}
		sb.AppendLine();

		foreach (Transform child in go.transform) {
			ListScripts(child.gameObject, sb, indentLevel + 1);
		}
	}
}
