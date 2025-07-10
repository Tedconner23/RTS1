using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntityTrackerItem : MonoBehaviour
{
    public int OwnedPlayer;
    public int[] VisibleToPlayers;
    public Vector3 Position;
    public GameObject RTSGameObject;
}