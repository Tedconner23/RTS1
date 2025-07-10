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
    public GameObject[] actionButtons;

    private RTSBuilding currentSelection;

    void Start()
    {
        toolbarPanel.SetActive(false); // Hide the toolbar at start
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
        if (currentSelection != null)
        {
            toolbarPanel.SetActive(true);
            unitNameText.text = currentSelection.BuildingName;
            unitStatsText.text = currentSelection.GetStats();

            for (int i = 0; i < actionButtons.Length; i++)
            {
                if (currentSelection.unitPrefabs != null && i < currentSelection.unitPrefabs.Length)
                {
                    actionButtons[i].SetActive(true);
                    var txt = actionButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                    if (txt != null)
                        txt.text = currentSelection.unitPrefabs[i].name;

                    int index = i;
                    var btn = actionButtons[i].GetComponent<Button>();
                    if (btn != null)
                    {
                        btn.onClick.RemoveAllListeners();
                        btn.onClick.AddListener(() => currentSelection.BuildUnit(index));
                    }
                }
                else
                {
                    actionButtons[i].SetActive(false);
                }
            }
        }
        else
        {
            toolbarPanel.SetActive(false);
            foreach (var button in actionButtons)
            {
                button.SetActive(false);
            }
        }
    }
}
