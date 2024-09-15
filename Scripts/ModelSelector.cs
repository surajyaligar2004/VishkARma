using UnityEngine;
using UnityEngine.UI;

public class ModelSelector : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Assign character prefabs in Inspector
    public GameObject planePrefab; // The detected plane prefab to place models on
    private GameObject selectedModel;

    void Start()
    {
        if (characterPrefabs == null || characterPrefabs.Length == 0)
        {
            UnityEngine.Debug.LogError("No character prefabs assigned to ModelSelector.");
            return;
        }

        // Example of adding listeners to buttons
        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            int index = i;
            Button button = transform.GetChild(i).GetComponent<Button>();
            button.onClick.AddListener(() => SelectCharacter(index));
        }
    }

    public void SelectCharacter(int index)
    {
        if (index >= 0 && index < characterPrefabs.Length)
        {
            selectedModel = characterPrefabs[index];
            UnityEngine.Debug.Log("Selected model: " + selectedModel.name);
        }
        else
        {
            UnityEngine.Debug.LogError("Invalid character index: " + index);
        }
    }

    public void PlaceModel(Vector3 position)
    {
        if (selectedModel != null)
        {
            Instantiate(selectedModel, position, Quaternion.identity);
        }
        else
        {
            UnityEngine.Debug.LogWarning("No model selected. Please select a character first.");
        }
    }
}