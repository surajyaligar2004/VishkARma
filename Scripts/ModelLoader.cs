using System.IO;
using UnityEngine;
using UnityEngine.UI; // Use Unity's UI system
using Debug = UnityEngine.Debug; // Ensure you're using Unity's Debug class

public class ModelLoader : MonoBehaviour
{
    public Transform content; // The content object inside the ScrollRect
    public GameObject buttonPrefab; // Button prefab for each model
    public string modelsFolder = "Assets/Models"; // Folder containing FBX files

    void Start()
    {
        LoadModels();
    }

    void LoadModels()
    {
        // Get all FBX files from the folder
        string[] fbxFiles = Directory.GetFiles(modelsFolder, "*.fbx");

        foreach (string fbxFile in fbxFiles)
        {
            // Load FBX as a GameObject (assuming it's already imported as an asset)
            GameObject model = LoadModelFromPath(fbxFile);

            // Create a button for this model
            GameObject button = Instantiate(buttonPrefab, content);

            // Set button text or image (if you have a thumbnail)
            button.GetComponentInChildren<UnityEngine.UI.Text>().text = model.name;

            // Add click listener to the button to select the model
            button.GetComponent<Button>().onClick.AddListener(() => OnModelSelected(model));
        }
    }

    GameObject LoadModelFromPath(string path)
    {
        // Load model from path (this could be adjusted if you're using AssetBundles)
        GameObject modelPrefab = Resources.Load<GameObject>(path);
        return Instantiate(modelPrefab);
    }

    void OnModelSelected(GameObject model)
    {
        // Handle what happens when the model is selected (e.g., place it in scene)
        Debug.Log($"Model {model.name} selected");
    }
}
