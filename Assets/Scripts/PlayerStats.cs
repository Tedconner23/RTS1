using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int playerTeamNum;
    public ResourceClass resourceGold = new ResourceClass(0f, 999999f, GameResource.Gold);
    public ResourceClass resourceLumber = new ResourceClass(0f, 999999f, GameResource.Lumber);
    public ResourceClass resourceSteel = new ResourceClass(0f, 999999f, GameResource.Steel);
    private int gameMaxPop;
    public int maxPop;
    public int curPop;


}

public class ResourceClass
{
    public float current;
    public float max;
    public GameResource type;

    public ResourceClass(float v1, float v2, GameResource gold)
    {
        V1 = v1;
        V2 = v2;
        Gold = gold;
    }

    public float V1 { get; }
    public float V2 { get; }
    public GameResource Gold { get; }
}
