using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DisplayTable
{
    public List<DisplayPoint> DisplayPoints;

    public bool IsTakeable = false;

    private void Start()
    {
        StartCoroutine(ProcessTakeToGuest());
    }

    private Transform GetEmptyPoint(Bread bread)
    {
        for (int i = 0; i < DisplayPoints.Count; i++)
        {
            if (DisplayPoints[i].IsActivate == false)
            {
                DisplayPoints[i].Activate(bread);

                return DisplayPoints[i].transform;
            }
        }
        return null;
    }

    private IEnumerator ProcessTakeToGuest()
    {
        yield return new WaitUntil(() => IsTakeable);

        Guest guest = GetNeedestGuest();

        int count = _currentCount;

        for (int i = 0; i < count; i++)
        {
            _currentCount--;

            int breadSID = GetTakeableBread();

            if (breadSID != -1)
            {
                ObjectPoolManager.Instance.DespawnBread(breadSID);
                SpawnToGuestHand(guest);
                yield return new WaitForSeconds(0.2f);
            }
        }

        StartCoroutine(ProcessTakeToGuest());
    }

    private int GetTakeableBread()
    {
        for (int i = 0; i < DisplayPoints.Count; i++)
        {
            if (DisplayPoints[i].IsActivate)
            {
                int sid = DisplayPoints[i].Bread.SID;
                DisplayPoints[i].Deactivate();
                return sid;
            }
        }
        return -1;
    }

    private Guest GetNeedestGuest()
    {
        Guest[] guests = GetTakeableGuests();

        int needCount = guests[0].MaxTakeBreadCount - guests[0].CurrentTakeCount;
        int guestIndex = 0;

        for (int i = 0; i < guests.Length; i++)
        {
            if (guests[i].MaxTakeBreadCount - guests[i].CurrentTakeCount <= needCount)
            {
                needCount = guests[i].MaxTakeBreadCount - guests[i].CurrentTakeCount;
                guestIndex = i;
            }
        }

        return guests[guestIndex];
    }

    private void SpawnToGuestHand(Guest guest)
    {
        Bread bread = ObjectPoolManager.Instance.Spawn(ObjectStates.OnHand);

        if (bread != null)
        {
            bread.Rigid.velocity = Vector3.zero;
            bread.transform.position = new Vector3(guest.TakePoint.position.x, guest.GetTakeY(), guest.TakePoint.position.z);
            bread.transform.rotation = Quaternion.identity;
            bread.HoldOn();
            bread.transform.parent = guest.transform;
            guest.AddBread(bread);
            guest.CarryOn();
            guest.RefreshText();
        }
    }
}