using UnityEngine;

[RequireComponent(typeof(GameEntityTrackerItem))]
public class FogOfWarRevealer : MonoBehaviour
{
    public float revealRadius = 5f;
    private GameEntityTrackerItem tracker;

    void Awake()
    {
        tracker = GetComponent<GameEntityTrackerItem>();
    }

    void Update()
    {
        if (FogOfWar.Instance != null && tracker != null)
        {
            FogOfWar.Instance.Reveal(transform.position, revealRadius, tracker.OwnedPlayer);
        }
    }
}
