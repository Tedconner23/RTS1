using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] initialBuildings;
    public GameObject[] initialUnits;

    public void SpawnPlayers(Vector3[] startPositions)
    {
        if (startPositions == null)
            return;

        for (int i = 0; i < startPositions.Length; i++)
        {
            Vector3 start = startPositions[i];
            foreach (var prefab in initialBuildings)
            {
                if (prefab != null)
                {
                    var obj = Instantiate(prefab, start, Quaternion.identity);
                    SetOwnership(obj, i);
                }
            }
            foreach (var unit in initialUnits)
            {
                if (unit != null)
                {
                    var uObj = Instantiate(unit, start + Vector3.right * 2f, Quaternion.identity);
                    SetOwnership(uObj, i);
                }
            }
        }
    }

    private void SetOwnership(GameObject obj, int player)
    {
        var unit = obj.GetComponent<RTSUnit>();
        if (unit != null)
        {
            unit.unitFaction = player;
        }

        var tracker = obj.GetComponent<GameEntityTrackerItem>();
        if (tracker != null)
        {
            tracker.OwnedPlayer = player;
        }
    }
}
