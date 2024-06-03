using System.Collections;
using UnityEngine;

public partial class DisplayTable
{
    private Player Player
    {
        get { return GameManager.Instance.Player; }
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

            Transform emptyPoint = GetEmptyPoint(bread);

            bread.transform.position = emptyPoint.position;
            bread.transform.rotation = Quaternion.identity;
            bread.HoldOn();
            bread.transform.parent = emptyPoint;
            bread.State = ObjectStates.OnDisplayTable;
        }
    }
}