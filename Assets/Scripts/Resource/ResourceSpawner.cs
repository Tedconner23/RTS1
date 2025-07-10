using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public ResourceNode[] resourcePrefabs;
    public int resourceCount = 20;
    public Vector2 mapSize = new Vector2(100f, 100f);
    public int clusterCount = 0;
    public int resourcesPerCluster = 5;
    public float clusterRadius = 5f;

    public void SpawnResources()
    {
        if (resourcePrefabs == null || resourcePrefabs.Length == 0)
            return;

        if (clusterCount > 0)
        {
            for (int c = 0; c < clusterCount; c++)
            {
                Vector3 centre = new Vector3(Random.Range(0f, mapSize.x), 0f, Random.Range(0f, mapSize.y));
                for (int i = 0; i < resourcesPerCluster; i++)
                {
                    SpawnAt(RandomPoint(centre, clusterRadius));
                }
            }
        }
        else
        {
            for (int i = 0; i < resourceCount; i++)
            {
                SpawnAt(RandomPoint(Vector3.zero, -1f));
            }
        }
    }

    private Vector3 RandomPoint(Vector3 centre, float radius)
    {
        if (radius > 0f)
        {
            Vector2 offset = Random.insideUnitCircle * radius;
            Vector3 pos = centre + new Vector3(offset.x, 0f, offset.y);
            pos.x = Mathf.Clamp(pos.x, 0f, mapSize.x);
            pos.z = Mathf.Clamp(pos.z, 0f, mapSize.y);
            return pos;
        }
        else
        {
            return new Vector3(Random.Range(0f, mapSize.x), 0f, Random.Range(0f, mapSize.y));
        }
    }

    private void SpawnAt(Vector3 pos)
    {
        var prefab = resourcePrefabs[Random.Range(0, resourcePrefabs.Length)];
        Instantiate(prefab, pos, Quaternion.identity, transform);
    }
}
