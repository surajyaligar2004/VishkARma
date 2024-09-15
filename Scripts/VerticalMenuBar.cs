using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VerticalMenuBar : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private float buttonSpacing = 10f;
    [SerializeField] private List<GameObject> characterModels;
    [SerializeField] private Transform arPlane;

    private List<Button> menuButtons = new List<Button>();

    private void Start()
    {
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        for (int i = 0; i < characterModels.Count; i++)
        {
            GameObject buttonObj = Instantiate(buttonPrefab, contentPanel);
            Button button = buttonObj.GetComponent<Button>();
            UnityEngine.UI.Text buttonText = buttonObj.GetComponentInChildren<UnityEngine.UI.Text>();

            if (buttonText != null)
            {
                buttonText.text = $"Model {i + 1}";
            }
            else
            {
                UnityEngine.Debug.LogWarning($"Text component not found on button {i}");
            }

            int index = i;
            button.onClick.AddListener(() => OnModelSelected(index));

            RectTransform rectTransform = buttonObj.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, -i * (rectTransform.rect.height + buttonSpacing));

            menuButtons.Add(button);
        }

        // Set content height to accommodate all buttons
        float totalHeight = characterModels.Count * (buttonPrefab.GetComponent<RectTransform>().rect.height + buttonSpacing);
        contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x, totalHeight);
    }

    private void OnModelSelected(int index)
    {
        if (index < 0 || index >= characterModels.Count)
        {
            UnityEngine.Debug.LogError($"Invalid model index: {index}");
            return;
        }

        // Remove any existing model on the AR plane
        foreach (Transform child in arPlane)
        {
            Destroy(child.gameObject);
        }

        // Instantiate the selected model on the AR plane
        GameObject selectedModel = Instantiate(characterModels[index], arPlane);
        selectedModel.transform.localPosition = Vector3.zero;
        selectedModel.transform.localRotation = Quaternion.identity;

        UnityEngine.Debug.Log($"Placed model {index} on AR plane");
    }
}