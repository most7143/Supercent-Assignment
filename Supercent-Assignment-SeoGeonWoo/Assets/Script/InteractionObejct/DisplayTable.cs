using System.Collections.Generic;
using UnityEngine;

public partial class DisplayTable : InteractionObejct
{
    public int MaxCount;

    private int _currentCount;

    private void Update()
    {
        if (TryTakeable())
        {
            IsTakeable = true;
        }
        else
        {
            IsTakeable = false;
        }
    }

    private bool TryTakeable()
    {
        if (_currentCount > 0)
        {
            int count = ObjectPoolManager.Instance.Guests.Count;
            for (int i = 0; i < count; i++)
            {
                if (ObjectPoolManager.Instance.Guests[i].CurrentMovePoint == GuestMovePoints.DisplayTable
                    && ObjectPoolManager.Instance.Guests[i].IsArrive)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private Guest[] GetTakeableGuests()
    {
        List<Guest> guests = new();

        for (int i = 0; i < ObjectPoolManager.Instance.Guests.Count; i++)
        {
            if (ObjectPoolManager.Instance.Guests[i].IsArrive
                && ObjectPoolManager.Instance.Guests[i].CurrentMovePoint == GuestMovePoints.DisplayTable)
            {
                guests.Add(ObjectPoolManager.Instance.Guests[i]);
            }
        }

        return guests.ToArray();
    }

    public override void Operate()
    {
        DropToTable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (TryOperate())
            {
                Operate();
            }
        }
    }

    private bool TryOperate()
    {
        if (Player.IsStack)
        {
            if (MaxCount > _currentCount)
            {
                return true;
            }
        }

        return false;
    }
}