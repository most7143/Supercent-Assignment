using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GuestMovePoints
{
    None,
    Center,
    DisplayTable,
    Counter,
    Exit,
    EatingTable,
}

public class GuestController : MonoBehaviour
{
    public Transform SpawnPoint;

    public Transform CenterPoint;

    public Transform DisplayTable;

    public List<Transform> DisplayPoints;

    private Dictionary<Transform, bool> _displayPoints = new();

    public Transform CounterPoint;

    public int MaxCount = 4;

    public int Interval = 1;

    private int _currentCount;

    private void Start()
    {
        SetDisplayPoints();

        StartCoroutine(ProcessSpawn());
    }

    private void SetDisplayPoints()
    {
        for (int i = 0; i < DisplayPoints.Count; i++)
        {
            _displayPoints.Add(DisplayPoints[i], false);
        }
    }

    private IEnumerator ProcessSpawn()
    {
        yield return new WaitForSeconds(Interval);

        Guest guest = ObjectPoolManager.Instance.SpawnGuest();

        guest.Controller = this;

        guest.SetTarget(CenterPoint);

        guest.transform.position = SpawnPoint.transform.position;

        _currentCount++;

        yield return new WaitUntil(() => _currentCount < MaxCount);

        StartCoroutine(ProcessSpawn());
    }

    public Transform NextPoints(Guest guest)
    {
        switch (guest.CurrentMovePoint)
        {
            case GuestMovePoints.Center:
                {
                    if (guest.MovePointCount == 0)
                    {
                        guest.CurrentMovePoint = GuestMovePoints.DisplayTable;
                        return GetEmptyDisplay();
                    }
                    else
                    {
                        guest.CurrentMovePoint = GuestMovePoints.Counter;
                        return CounterPoint;
                    }
                }
            case GuestMovePoints.DisplayTable:
                {
                    guest.CurrentMovePoint = GuestMovePoints.Center;
                    return CenterPoint;
                }
            case GuestMovePoints.Counter:
                {
                    guest.CurrentMovePoint = GuestMovePoints.Exit;
                    return SpawnPoint;
                }
        }

        return null;
    }

    private Transform GetEmptyDisplay()
    {
        for (int i = 0; i < _displayPoints.Count; i++)
        {
            if (_displayPoints[DisplayPoints[i]] == false)
            {
                _displayPoints[DisplayPoints[i]] = true;

                return DisplayPoints[i];
            }
        }

        return null;
    }
}