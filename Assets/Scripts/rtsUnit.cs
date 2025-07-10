using UnityEngine;
using UnityEngine.AI;

public class RTSUnit : GameEntity, RTSISelectable
{
    private Vector3 m_Position;
    private NavMeshAgent navMeshAgent;

    Vector3 RTSISelectable.Position
    {
        get => m_Position;
        set
        {
            m_Position = value;
            position = value;
        }
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on RTSUnit.");
        }
    }

    public void Select()
    {
        gameObject.transform.position = m_Position;
        // Implement selection logic (e.g., highlight the unit)
        Debug.Log($"{gameObject.name} selected.");
    }

    public void Deselect()
    {
        // Implement deselection logic (e.g., remove highlight)
        Debug.Log($"{gameObject.name} deselected.");
    }

    public void Spawn(Vector3 spawnPosition, float checkRadius)
    {
        m_Position = AdjustPositionIfCollides(spawnPosition, checkRadius);
        position = m_Position;
        transform.position = m_Position;
    }

    public void Spawn(Vector3 spawnPosition, float checkRadius, Vector3 rallyPoint)
    {
        Spawn(spawnPosition, checkRadius);
        MoveToPosition(rallyPoint);
    }

    private Vector3 AdjustPositionIfCollides(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        while (colliders.Length > 0)
        {
            position += new Vector3(radius * 2, 0, 0); // Adjust position by moving to the side
            colliders = Physics.OverlapSphere(position, radius);
        }
        return position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right-click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                MoveToPosition(hit.point);
            }
        }
    }

    private void MoveToPosition(Vector3 position)
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(position);
        }
    }

    public string GetStats()
    {
        return $"HP: {health}\nArmor: {armor}";
    }
}
