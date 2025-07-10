using AmplifyShaderEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject HQPrefab;
    public GameObject PlayerObjects;
    public void SpawnHQ()
    {
        var spawn  = Instantiate(HQPrefab);
        spawn.transform.localScale = Vector3.one;
        spawn.transform.localPosition = Vector3.one;
        spawn.transform.position = Vector3.one;
        spawn.GetComponent<RTSBuilding>().PrebuildHighlight();
    }
}
