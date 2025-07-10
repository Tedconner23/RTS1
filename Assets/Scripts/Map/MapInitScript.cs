using UnityEngine;
using System;

public class MapInitScript : MonoBehaviour
{
    // Public variables to set the grid size
    public int limitX = 100;
    public int limitY = 100;
    public GameObject terrainParent;
    public Vector3[] PlayerStartPositions;

    // Prefab for the quad (can be set in the Unity Editor)
    public GameObject quadPrefab;

    // Variable to store the center of the grid
    public Vector3 gridCenter;

    // Event to notify when the map is initialized
    public event Action OnMapInitialized;

    void Start()
    {
        // Initialize the map
        InitMap();
        CommonVariables.PlayerCount = PlayerStartPositions.Length;
        // Calculate the center of the grid
        CalculateGridCenter();

        // Notify subscribers that the map is initialized
        OnMapInitialized?.Invoke();
    }

    void InitMap()
    {
        // Loop through x and y limits to generate the grid
        for (int x = 0; x < limitX; x++)
        {
            for (int y = 0; y < limitY; y++)
            {
                // Calculate the position for the new quad
                Vector3 position = new Vector3(x, 0, y);

                // Instantiate the quad at the calculated position
                GameObject a = Instantiate(quadPrefab, position, Quaternion.Euler(90f,0f,0f));
                a.transform.parent = terrainParent.transform;
            }
        }
    }

    void CalculateGridCenter()
    {
        // Calculate the center position based on the grid limits
        float centerX = (limitX - 1) / 2.0f;
        float centerY = (limitY - 1) / 2.0f;

        // Set the grid center position
        gridCenter = new Vector3(centerX, 0, centerY);
    }
}
