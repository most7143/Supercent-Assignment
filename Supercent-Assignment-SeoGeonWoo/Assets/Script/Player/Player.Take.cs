using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public Transform TakePoint;

    public int CurrentTakeCount { get; set; }

    public int TakeBreadY;

    public List<Bread> Breads;

    public float GetTakeY()
    {
        return TakePoint.position.y + (CurrentTakeCount * 0.5f);
    }

    public void AddBread(Bread bread)
    {
        Breads.Add(bread);
        CurrentTakeCount++;
    }

    public void RemoveBread()
    {
        Bread bread = Breads[Breads.Count - 1];

        Breads.Remove(bread);

        CurrentTakeCount--;

        ObjectPoolManager.Instance.Despawn(bread);
    }
}