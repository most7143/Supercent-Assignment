using System.Collections;
using UnityEngine;

public class CounterTable : InteractionObejct
{
    public Transform PaperPoint;

    public MoneyTrigger MoneyTrigger;

    public bool IsOperating { get; set; }

    public bool IsCloseBag { get; set; }

    private PaperBag paperBag;

    public override void Operate()
    {
        IsOperating = true;
        SpawnBag();
    }

    public bool TryOperate()
    {
        if (ObjectPoolManager.Instance.FindArrivedGuest(GuestMovePoints.Counter) != null
            && false == IsOperating)
        {
            return true;
        }

        return false;
    }

    public void SpawnBag()
    {
        IsCloseBag = false;
        paperBag = ObjectPoolManager.Instance.SpawnPaperBag();
        paperBag.transform.position = PaperPoint.transform.position;
        paperBag.transform.rotation = Quaternion.identity;
    }

    public IEnumerator ProcessClose(Guest guest)
    {
        paperBag.Close();
        yield return new WaitForSeconds(0.5f);
        IsOperating = false;
        paperBag.transform.parent = guest.transform;
        paperBag.transform.position = guest.TakePoint.position;
        IsCloseBag = true;
    }
}