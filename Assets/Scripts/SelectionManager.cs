using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 startPos;
    private Vector2 endPos;
    private bool isDragging;

    private List<RTSISelectable> selectedObjects = new List<RTSISelectable>();

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            startPos = Mouse.current.position.ReadValue();
            isDragging = true;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            endPos = Mouse.current.position.ReadValue();
            if (isDragging)
            {
                SelectObjects();
                isDragging = false;
            }
        }

        if (isDragging)
        {
            // Optionally, display drag selection rectangle
        }
    }

    void SelectObjects()
    {
        // Clear previous selection
        foreach (var obj in selectedObjects)
        {
            obj.Deselect();
        }
        selectedObjects.Clear();

        // Single click selection
        if (Vector2.Distance(startPos, endPos) < 10f)
        {
            Ray ray = mainCamera.ScreenPointToRay(startPos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                RTSISelectable selectable = hit.collider.GetComponent<RTSISelectable>();
                if (selectable != null)
                {
                    selectable.Select();
                    selectedObjects.Add(selectable);
                }
            }
        }
        // Drag selection
        else
        {
            Rect selectionRect = new Rect(
                Mathf.Min(startPos.x, endPos.x),
                Mathf.Min(startPos.y, endPos.y),
                Mathf.Abs(startPos.x - endPos.x),
                Mathf.Abs(startPos.y - endPos.y));

            foreach (var selectable in FindObjectsOfType<MonoBehaviour>().OfType<RTSISelectable>())
            {
                Vector3 screenPos = mainCamera.WorldToScreenPoint(selectable.Position);
                if (selectionRect.Contains(screenPos, true))
                {
                    selectable.Select();
                    selectedObjects.Add(selectable);
                }
            }
        }

        // Log selected objects
        foreach (var obj in selectedObjects)
        {
            Debug.Log($"{obj} selected.");
        }
    }
}
