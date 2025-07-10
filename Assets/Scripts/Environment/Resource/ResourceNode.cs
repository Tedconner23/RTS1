using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public GameResource resourceType = GameResource.Gold;
    public float amount = 500f;

    public float Harvest(float value)
    {
        float collected = Mathf.Min(value, amount);
        amount -= collected;
        if (amount <= 0f)
        {
            Destroy(gameObject);
        }
        return collected;
    }
}
