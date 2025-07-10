using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public static FogOfWar Instance { get; private set; }

    public int mapWidth = 100;
    public int mapHeight = 100;
    public float cellSize = 1f;

    private bool[,,] visibility;

    void Awake()
    {
        Instance = this;
        InitVisibility(mapWidth, mapHeight);
    }

    public void InitVisibility(int width, int height)
    {
        mapWidth = width;
        mapHeight = height;
        visibility = new bool[CommonVariables.PlayerCount, mapWidth, mapHeight];
    }

    public void Reveal(Vector3 worldPos, float radius, int player)
    {
        if (player < 0 || player >= CommonVariables.PlayerCount)
            return;

        int cx = Mathf.RoundToInt(worldPos.x / cellSize);
        int cy = Mathf.RoundToInt(worldPos.z / cellSize);
        int r = Mathf.RoundToInt(radius / cellSize);
        for (int x = -r; x <= r; x++)
        {
            for (int y = -r; y <= r; y++)
            {
                int px = cx + x;
                int py = cy + y;
                if (px >= 0 && py >= 0 && px < mapWidth && py < mapHeight)
                    visibility[player, px, py] = true;
            }
        }
    }

    public bool IsVisible(Vector3 worldPos, int player)
    {
        if (player < 0 || player >= CommonVariables.PlayerCount)
            return false;
        int px = Mathf.RoundToInt(worldPos.x / cellSize);
        int py = Mathf.RoundToInt(worldPos.z / cellSize);
        if (px < 0 || py < 0 || px >= mapWidth || py >= mapHeight)
            return false;
        return visibility[player, px, py];
    }
}
