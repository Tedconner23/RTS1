using UnityEngine;
using UnityEngine.AI;

public class RTSUnit : GameEntity, RTSISelectable
{
    private Vector3 m_Position;
    private NavMeshAgent navMeshAgent;
    private GameEntityTrackerItem tracker;
    private FogOfWarRevealer revealer;

    Vector3 RTSISelectable.Position
    {
        get => m_Position;
        set
        {
            m_Position = value;
            position = value;
        }
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on RTSUnit.");
        }

        // Calculate size based on attached colliders
        sizeRadius = PlacementHelper.GetBoundingRadius(gameObject);
        sizeCategory = PlacementHelper.DetermineCategory(sizeRadius);
        placementRadius = PlacementHelper.GetMaxRadius(sizeCategory);
        PlacementHelper.EnsureCollider(gameObject, placementRadius);
        if (navMeshAgent != null)
        {
            navMeshAgent.radius = placementRadius;
        }

        tracker = GetComponent<GameEntityTrackerItem>();
        if (tracker == null)
            tracker = gameObject.AddComponent<GameEntityTrackerItem>();
        tracker.OwnedPlayer = unitFaction;

        revealer = GetComponent<FogOfWarRevealer>();
        if (revealer == null)
            revealer = gameObject.AddComponent<FogOfWarRevealer>();
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
        float radius = placementRadius;
        if (checkRadius > radius)
            radius = checkRadius;

        m_Position = AdjustPositionIfCollides(spawnPosition, radius);
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
        Collider[] colliders = Physics.OverlapSphere(position, radius, ~0, QueryTriggerInteraction.Ignore);
        int tries = 0;
        while (colliders.Length > 0 && tries < 10)
        {
            position += new Vector3(radius * 2, 0, 0);
            colliders = Physics.OverlapSphere(position, radius, ~0, QueryTriggerInteraction.Ignore);
            tries++;
        }
        if (tries >= 10)
        {
            Debug.LogWarning("Unable to find free space to spawn unit.");
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
