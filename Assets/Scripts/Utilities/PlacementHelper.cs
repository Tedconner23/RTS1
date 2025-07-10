using UnityEngine;

public enum SizeCategory
{
    Small,
    Medium,
    Large
}

public static class PlacementHelper
{
    private static readonly float[] MaxSizePerCategory =
    {
        1.5f, // Small
        3f,   // Medium
        6f    // Large
    };

    public static float GetMaxRadius(SizeCategory size)
    {
        int i = (int)size;
        if (i < 0 || i >= MaxSizePerCategory.Length)
            return 1.5f;
        return MaxSizePerCategory[i];
    }

    public static float GetSpacing(SizeCategory size, float currentRadius)
    {
        float target = GetMaxRadius(size);
        float spacing = target - currentRadius;
        return Mathf.Max(0f, spacing);
    }

    public static SizeCategory DetermineCategory(float radius)
    {
        if (radius <= GetMaxRadius(SizeCategory.Small))
            return SizeCategory.Small;
        if (radius <= GetMaxRadius(SizeCategory.Medium))
            return SizeCategory.Medium;
        return SizeCategory.Large;
    }

    public static float GetBoundingRadius(GameObject obj)
    {
        Bounds bounds = new Bounds(obj.transform.position, Vector3.zero);
        bool hasBounds = false;

        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        foreach (var c in colliders)
        {
            if (!hasBounds)
            {
                bounds = c.bounds;
                hasBounds = true;
            }
            else
            {
                bounds.Encapsulate(c.bounds);
            }
        }

        if (!hasBounds)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                if (!hasBounds)
                {
                    bounds = r.bounds;
                    hasBounds = true;
                }
                else
                {
                    bounds.Encapsulate(r.bounds);
                }
            }
        }

        if (!hasBounds)
            return 0.5f;

        return Mathf.Max(bounds.extents.x, Mathf.Max(bounds.extents.y, bounds.extents.z));
    }

    public static bool CanPlace(GameEntity entity, Vector3 position)
    {
        float radius = GetMaxRadius(entity.sizeCategory);
        Collider[] hits = Physics.OverlapSphere(position, radius, ~0, QueryTriggerInteraction.Ignore);
        foreach (var hit in hits)
        {
            if (hit.transform != entity.transform && !hit.transform.IsChildOf(entity.transform))
                return false;
        }
        return true;
    }
}
