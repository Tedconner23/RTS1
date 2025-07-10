using UnityEngine;

public enum SizeCategory
{
    Small,
    Medium,
    Large
}

public static class PlacementHelper
{
    public static float GetSpacing(SizeCategory size)
    {
        switch (size)
        {
            case SizeCategory.Small: return 0.5f;
            case SizeCategory.Medium: return 1f;
            case SizeCategory.Large: return 2f;
            default: return 0.5f;
        }
    }

    public static float GetBoundingRadius(GameObject obj)
    {
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        if (colliders.Length == 0)
            return 0.5f;
        Bounds b = colliders[0].bounds;
        foreach (var c in colliders)
            b.Encapsulate(c.bounds);
        return Mathf.Max(b.extents.x, Mathf.Max(b.extents.y, b.extents.z));
    }

    public static bool CanPlace(GameEntity entity, Vector3 position)
    {
        float radius = entity.sizeRadius + GetSpacing(entity.sizeCategory);
        Collider[] hits = Physics.OverlapSphere(position, radius, ~0, QueryTriggerInteraction.Ignore);
        foreach (var hit in hits)
        {
            if (hit.transform != entity.transform && !hit.transform.IsChildOf(entity.transform))
                return false;
        }
        return true;
    }
}
