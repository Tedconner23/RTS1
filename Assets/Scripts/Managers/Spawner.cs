using AmplifyShaderEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // List of available building prefabs that can be spawned from the toolbar
    public List<GameObject> buildingPrefabs = new List<GameObject>();

    // Optional list of units that can be spawned. Currently used when a
    // building is selected and the toolbar generates unit buttons.
    public List<GameObject> unitPrefabs = new List<GameObject>();

    // Parent object under which spawned objects will be organised.  This can be
    // left null in which case the objects will be created at the root of the
    // scene.
    public GameObject PlayerObjects;

    /// <summary>
    /// Spawn a building from <see cref="buildingPrefabs"/> by index.
    /// </summary>
    /// <param name="index">Index of the building prefab.</param>
    public void SpawnBuilding(int index)
    {
        if (index < 0 || index >= buildingPrefabs.Count)
            return;

        var prefab = buildingPrefabs[index];
        var parent = PlayerObjects != null ? PlayerObjects.transform : null;
        var spawn = Instantiate(prefab, parent);
        spawn.transform.localScale = Vector3.one;
        spawn.transform.localPosition = Vector3.one;
        spawn.transform.position = Vector3.one;

        var building = spawn.GetComponent<RTSBuilding>();
        if (building != null)
        {
            building.PrebuildHighlight();
        }
    }
}
