using UnityEngine;

public partial class Player
{
    public Transform TakePoint;

    public int CurrentTakeCount { get; set; }

    public int TakeBreadY;

    public float GetTakeY()
    {
        return TakePoint.position.y + (CurrentTakeCount * 0.5f);
    }
}