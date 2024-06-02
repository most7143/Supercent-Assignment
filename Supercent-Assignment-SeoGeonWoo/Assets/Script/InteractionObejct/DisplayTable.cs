using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTable : InteractionObejct
{
    public List<DisplayPoint> DisplayPoints;

    public int MaxCount;

    private int _currentCount;

    private Player Player
    {
        get { return GameManager.Instance.Player; }
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

    private void DropToTable()
    {
        GameManager.Instance.LockMove();
        StartCoroutine(ProcessDrop());
    }

    private IEnumerator ProcessDrop()
    {
        int count = MaxCount - _currentCount;

        for (int i = 0; i <= count; i++)
        {
            Player.RemoveBread();
            SpawnToTable();
            _currentCount++;

            if (Player.CurrentTakeCount == 0 || MaxCount == _currentCount)
            {
                break;
            }

            yield return new WaitForSeconds(0.2f);
        }

        GameManager.Instance.UnlockMove();

        if (Player.CurrentTakeCount == 0)
        {
            Player.CarryOff();
        }
    }

    private void SpawnToTable()
    {
        Bread bread = ObjectPoolManager.Instance.SpawnBread();

        if (bread != null)
        {
            bread.Rigid.velocity = Vector3.zero;

            Transform emptyPoint = GetEmptyPoint();
            bread.transform.position = emptyPoint.position;
            bread.transform.rotation = Quaternion.identity;
            bread.HoldOn();
            bread.transform.parent = emptyPoint;
        }
    }

    private Transform GetEmptyPoint()
    {
        for (int i = 0; i < DisplayPoints.Count; i++)
        {
            if (DisplayPoints[i].IsActivate == false)
            {
                DisplayPoints[i].IsActivate = true;

                return DisplayPoints[i].transform;
            }
        }
        return null;
    }
}