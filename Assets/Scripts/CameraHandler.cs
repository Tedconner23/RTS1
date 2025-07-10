using System.Linq;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    // Reference to the MapInitScript to get the grid center
    public MapInitScript mapInitScript;
    
    // Camera height and rotation parameters
    public float cameraHeight = 5.0f;
    public float cameraMinHeight = 2.0f;
    public float cameraMaxHeight = 10.0f;
    public float cameraXRotMin = 45f;
    public float cameraXRotMax = 60f;

    public float moveSpeed = 1.0f;
    public float rotSpeed = 7.0f;

    void Start()
    {
        // Check if the mapInitScript is assigned
        if (mapInitScript == null)
        {
            Debug.LogError("MapInitScript reference is not set in CameraHandler.");
            return;
        }

        // Subscribe to the map initialized event
        mapInitScript.OnMapInitialized += CenterCameraOnGrid;
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when the script is destroyed
        if (mapInitScript != null)
        {
            mapInitScript.OnMapInitialized -= CenterCameraOnGrid;
        }
    }

    void CenterCameraOnGrid()
    {
        // Get the center position of the grid from MapInitScript
        Vector3 playerStartPost = mapInitScript.PlayerStartPositions.First();
        
        // Set the camera position to the center of the grid with a specified height
        transform.position = new Vector3(playerStartPost.x, cameraHeight, playerStartPost.z);

        // Adjust the camera rotation to look down at the grid
        UpdateCameraRotation();
    }

    public void MoveCamera(Vector3 direction)
    {
        transform.position += direction * moveSpeed;

        // Clamp the camera height between the min and max values
        float clampedHeight = Mathf.Clamp(transform.position.y, cameraMinHeight, cameraMaxHeight);
        transform.position = new Vector3(transform.position.x, clampedHeight, transform.position.z);

        // Update the camera rotation based on the new height
        UpdateCameraRotation();
    }

    public void RotateCamera(Vector3 direction)
    {
        transform.localEulerAngles += direction * rotSpeed;
    }

    private void UpdateCameraRotation()
    {
        // Calculate the x rotation angle based on the camera height
        float t = (transform.position.y - cameraMinHeight) / (cameraMaxHeight - cameraMinHeight);
        float xRotation = Mathf.Lerp(cameraXRotMin, cameraXRotMax, t);

        // Update the camera rotation
        transform.rotation = Quaternion.Euler(xRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
