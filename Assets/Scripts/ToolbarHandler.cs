using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Assuming you're using UI elements like buttons and text

public class ToolbarHandler : MonoBehaviour
{
    // UI elements for displaying selected object info
    public GameObject toolbarPanel;
    public TextMeshProUGUI unitNameText;
    public TextMeshProUGUI unitStatsText;

    // Prefab used for dynamically creating buttons in the toolbar
    public GameObject buttonPrefab;

    // Parent transform for the generated buttons.  If left null the toolbar
    // panel transform will be used.
    public Transform buttonParent;

    // Additional spacing between buttons when laying them out horizontally.
    public float buttonSpacing = 10f;

    // Cached data from the button prefab for layout calculations
    private Vector2 prefabStartPos;
    private Vector2 prefabSize;

    private RTSBuilding currentSelection;

    private Spawner spawner;
    private readonly List<GameObject> spawnedButtons = new List<GameObject>();

    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        if (buttonPrefab != null)
        {
            var rect = buttonPrefab.GetComponent<RectTransform>();
            if (rect != null)
            {
                prefabStartPos = rect.anchoredPosition;
                prefabSize = rect.sizeDelta;
            }
        }
        UpdateToolbar();
    }

    void Update()
    {
        // Any update logic for the toolbar can go here, if needed
    }

    public void OnSelect(RTSBuilding selectedObject)
    {
        currentSelection = selectedObject;
        UpdateToolbar();
    }

    public void OnDeselect()
    {
        currentSelection = null;
        UpdateToolbar();
    }

    private void UpdateToolbar()
    {
        // Remove any existing dynamic buttons
        foreach (var button in spawnedButtons)
        {
            if (button != null)
                Destroy(button);
        }
        spawnedButtons.Clear();

        Transform parent = buttonParent != null ? buttonParent : toolbarPanel.transform;

        if (currentSelection != null)
        {
            // Display information about the selected building
            toolbarPanel.SetActive(true);
            unitNameText.text = currentSelection.BuildingName;
            unitStatsText.text = currentSelection.GetStats();

            if (currentSelection.unitPrefabs != null)
            {
                for (int i = 0; i < currentSelection.unitPrefabs.Length; i++)
                {
                    int idx = i;
                    GameObject buttonObj = Instantiate(buttonPrefab, parent);
                    PositionButton(buttonObj.GetComponent<RectTransform>(), idx);

                    var txt = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
                    if (txt != null)
                        txt.text = currentSelection.unitPrefabs[i].name;

                    var btn = buttonObj.GetComponent<Button>();
                    if (btn != null)
                    {
                        btn.onClick.RemoveAllListeners();
                        btn.onClick.AddListener(() => currentSelection.BuildUnit(idx));
                    }

                    spawnedButtons.Add(buttonObj);
                }
            }
        }
        else
        {
            // Nothing selected: show building spawn buttons
            toolbarPanel.SetActive(true);
            unitNameText.text = string.Empty;
            unitStatsText.text = string.Empty;

            if (spawner != null)
            {
                for (int i = 0; i < spawner.buildingPrefabs.Count; i++)
                {
                    int idx = i;
                    GameObject buttonObj = Instantiate(buttonPrefab, parent);
                    PositionButton(buttonObj.GetComponent<RectTransform>(), idx);

                    var txt = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
                    if (txt != null)
                        txt.text = spawner.buildingPrefabs[i].name;

                    var btn = buttonObj.GetComponent<Button>();
                    if (btn != null)
                    {
                        btn.onClick.RemoveAllListeners();
                        btn.onClick.AddListener(() => spawner.SpawnBuilding(idx));
                    }

                    spawnedButtons.Add(buttonObj);
                }
            }
        }
    }

    private void PositionButton(RectTransform rect, int index)
    {
        if (rect == null)
            return;

        rect.sizeDelta = prefabSize;
        rect.anchoredPosition = prefabStartPos + new Vector2(index * (prefabSize.x + buttonSpacing), 0f);
    }
}
