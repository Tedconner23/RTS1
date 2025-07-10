using UnityEngine;

public interface RTSISelectable
{
    public Vector3 Position { get; set; }
    void Select();
    void Deselect();
}