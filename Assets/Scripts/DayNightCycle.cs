using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;
    public float rotationSpeed = 10f;

    void Update()
    {
        if (directionalLight != null)
        {
            directionalLight.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
    }
}
