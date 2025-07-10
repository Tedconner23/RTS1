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
            // Show the toolbar and update the UI elements with the selected object's data
            toolbarPanel.SetActive(true);

            // Assuming RTSISelectable has a Name property and a method to get stats
            unitNameText.text = currentSelection.BuildingName;
            unitStatsText.text = currentSelection.GetStats();

            // Enable or update action buttons as needed
            for (int i = 0; i < actionButtons.Length; i++)
            {
                // Assuming actionButtons correspond to actions the unit can take
                actionButtons[i].SetActive(true); // Show buttons
                // Set button text or icon based on the currentSelection's available actions
            }
        }
        else
        {
            // Hide the toolbar and disable buttons
            toolbarPanel.SetActive(false);
            foreach (var button in actionButtons)
            {
                button.SetActive(false);
            }
        }
    }
}
