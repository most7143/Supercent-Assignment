using System.Collections.Generic;
using UnityEngine;

public enum ObjectTypes
{
    None,
    Quest,
    Bread,
    Money
}

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager _instance;

    public Dictionary<int, Bread> Breads = new();

    public GameObject Bread;

    public Dictionary<int, Guest> Guests = new();

    public GameObject Guest;

    public Dictionary<int, PaperBag> PaperBags = new();

    public GameObject PaperBag;

    public GameObject Money;

    public Dictionary<int, Money> Moneies = new();

    public static ObjectPoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ObjectPoolManager();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        Init();
    }

    private void Init()
    {
        for (int i = 0; i < 30; i++)
        {
            Bread bread = Instantiate(Bread, Vector3.zero, Quaternion.identity).GetComponent<Bread>();

            bread.transform.parent = transform;
            Breads.Add(i, bread);
            bread.Init(i);
        }

        for (int i = 0; i < 10; i++)
        {
            Guest guest = Instantiate(Guest, Vector3.zero, Quaternion.identity).GetComponent<Guest>();
            guest.transform.parent = transform;
            Guests.Add(i, guest);
            guest.Init();
        }

        for (int i = 0; i < 10; i++)
        {
            PaperBag paperBag = Instantiate(PaperBag, Vector3.zero, Quaternion.identity).GetComponent<PaperBag>();
            paperBag.Init();
            paperBag.transform.parent = transform;
            PaperBags.Add(i, paperBag);
        }

        for (int i = 0; i < 60; i++)
        {
            Money money = Instantiate(Money, Vector3.zero, Quaternion.identity).GetComponent<Money>();
            money.Init();
            money.transform.parent = transform;
            Moneies.Add(i, money);
        }
    }

    public int FindSID(ObjectStates state)
    {
        for (int i = 0; i < Breads.Count; i++)
        {
            if (Breads[i].State == state)
            {
                return Breads[i].SID;
            }
        }

        return -1;
    }

    public Bread SpawnBread()
    {
        for (int i = 0; i <= Breads.Count; i++)
        {
            if (false == Breads[i].IsActivate)
            {
                Breads[i].Activate();
                return Breads[i];
            }
        }

        return null;
    }

    public Guest SpawnGuest()
    {
        for (int i = 0; i <= Guests.Count; i++)
        {
            if (false == Guests[i].IsActivate)
            {
                Guests[i].Activate();
                return Guests[i];
            }
        }

        return null;
    }

    public PaperBag SpawnPaperBag()
    {
        for (int i = 0; i <= PaperBags.Count; i++)
        {
            if (false == PaperBags[i].IsActivate)
            {
                PaperBags[i].Activate();
                return PaperBags[i];
            }
        }

        return null;
    }

    public Money SpawnMoney()
    {
        for (int i = 0; i < Moneies.Count; i++)
        {
            if (false == Moneies[i].IsActivate)
            {
                Moneies[i].Activate();
                return Moneies[i];
            }
        }

        return null;
    }

    public Bread Spawn(ObjectStates state)
    {
        for (int i = 0; i <= Breads.Count; i++)
        {
            if (false == Breads[i].IsActivate)
            {
                Breads[i].Activate();
                Breads[i].State = state;
                return Breads[i];
            }
        }

        return null;
    }

    public Guest FindArrivedGuest(GuestMovePoints guestMove)
    {
        for (int i = 0; i < Guests.Count; i++)
        {
            if (Guests[i].IsArrive)
            {
                if (Guests[i].CurrentMovePoint == guestMove)
                {
                    return Guests[i];
                }
            }
        }

        return null;
    }

    public void DespawnBread(int sid)
    {
        if (sid != -1)
        {
            Breads[sid].Deactivate();
        }
    }

    public void Despawn(Bread bread)
    {
        for (int i = 0; i <= Breads.Count; i++)
        {
            if (Breads[i] == bread)
            {
                Breads[i].Deactivate();
                break;
            }
        }
    }

    public void Despawn(Guest guest)
    {
        for (int i = 0; i <= Guests.Count; i++)
        {
            if (Guests[i] == guest)
            {
                Guests[i].Deactivate();

                break;
            }
        }
    }

    public void Despawn(PaperBag paperBag)
    {
        for (int i = 0; i <= PaperBags.Count; i++)
        {
            if (PaperBags[i] == paperBag)
            {
                PaperBags[i].Deactivate();
                break;
            }
        }
    }

    public void Despawn(Money money)
    {
        for (int i = 0; i <= Moneies.Count; i++)
        {
            if (Moneies[i] == money)
            {
                Moneies[i].Deactivate();
                break;
            }
        }
    }
}