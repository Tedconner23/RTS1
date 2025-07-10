using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RTSBuilding : MonoBehaviour, RTSISelectable
{
    private Vector3 m_Position;
    public GameObject ActivateObject;
    public GameObject BuildObject;
    public GameObject DeactiveObject;
    public GameObject SelectRing;
    public GameObject BuildOverlayObject;
    public float BuildTimer;
    public string BuildingName;
    public BuildingType[] buildingTypes;
    public float BuildProgress;
    public Camera UnitCam;

    public string GetStats()
    {
        return "test";
    }
    Vector3 RTSISelectable.Position
    {
        get => m_Position;
        set => m_Position = value;
    }

    public void PrebuildHighlight()
    {
        ActivateObject.SetActive(false);
        BuildObject.SetActive(false);
        DeactiveObject.SetActive(false);
        SelectRing.SetActive(false);
        BuildOverlayObject.SetActive(true);
        StartCoroutine(MoveWhileHighlight());
    }

    IEnumerator MoveWhileHighlight()
    {
        while (true)
        {
            if(ActivateObject.activeInHierarchy || BuildObject.activeInHierarchy || DeactiveObject.activeInHierarchy || SelectRing.activeInHierarchy)
            {
                ActivateObject.SetActive(false);
                BuildObject.SetActive(false);
                DeactiveObject.SetActive(false);
                SelectRing.SetActive(false);
            }
            if (Input.GetMouseButtonDown(0)) // Left-click to place
            {
                BuildOverlayObject.SetActive(false);
                ActivateObject.SetActive(false);
                BuildObject.SetActive(true);
                DeactiveObject.SetActive(false);
                SelectRing.SetActive(false);
                StartCoroutine(BuildProgressCoroutine());
                yield break;
            }
            else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape)) // Right-click or Esc to cancel
            {
                DeactivateAllObjects();
                yield break;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 newPosition = hit.point;
                newPosition.y = 0;
                transform.position = newPosition;
            }

            yield return null;
        }
    }

    IEnumerator BuildProgressCoroutine()
    {
        var buildTime = 0f;
        while (buildTime < BuildTimer)
        {

            buildTime += Time.deltaTime;
            BuildProgress = buildTime / BuildTimer;
            yield return null;
        }
        ActivateObject.SetActive(true);
        BuildObject.SetActive(false);
        yield return null;
    }

    public void Select()
    {
        UnitCam.cullingMask = 1;
        SelectRing.SetActive(true);
        m_Position = gameObject.transform.position;
        // Implement selection logic (e.g., highlight the building)
        Debug.Log($"{gameObject.name} selected.");
    }

    public void Deselect()
    {
        UnitCam.cullingMask = 0;
        SelectRing.SetActive(false);
        // Implement deselection logic (e.g., remove highlight)
        Debug.Log($"{gameObject.name} deselected.");
    }

    private void DeactivateAllObjects()
    {
        ActivateObject.SetActive(false);
        BuildObject.SetActive(false);
        DeactiveObject.SetActive(false);
        SelectRing.SetActive(false);
        BuildOverlayObject.SetActive(false);
    }
}
