using UnityEngine;

public class SimpleMapGenerator : MonoBehaviour
{
    public int width = 100;
    public int height = 100;
    public float scale = 5f;
    public float maxHeight = 5f;
    public ResourceSpawner resourceSpawner;
    public int resourceClusters = 3;
    public int resourcesPerCluster = 5;
    public int seed = 0;

    public void Generate()
    {
        Terrain terrain = GetComponent<Terrain>();
        if (terrain != null)
        {
            TerrainData data = terrain.terrainData;
            data.heightmapResolution = Mathf.Max(width, height) + 1;
            float[,] heights = new float[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float nx = ((float)x / width * scale) + seed;
                    float ny = ((float)y / height * scale) + seed;
                    heights[y, x] = Mathf.PerlinNoise(nx, ny) * (maxHeight / data.size.y);
                }
            }
            data.size = new Vector3(width, maxHeight, height);
            data.SetHeights(0, 0, heights);
        }

        if (resourceSpawner != null)
        {
            resourceSpawner.mapSize = new Vector2(width, height);
            resourceSpawner.clusterCount = resourceClusters;
            resourceSpawner.resourcesPerCluster = resourcesPerCluster;
            resourceSpawner.SpawnResources();
        }
    }
}
