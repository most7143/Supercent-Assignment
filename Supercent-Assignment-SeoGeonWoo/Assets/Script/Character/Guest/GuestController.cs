using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GuestMovePoints
{
    None,
    Center,
    DisplayTable,
    Counter,
    CounterReturn,
    EatingWaitPoint,
    EatingTable,
    Exit,
}

public class GuestController : MonoBehaviour
{
    public Transform SpawnPoint;

    public Transform CenterPoint;

    public DisplayTable DisplayTable;

    public Transform CounterPoint;

    public Transform CounterReturnPoint;

    public Transform EatingWaitPoint;

    public Transform EatingSpacePoint;

    public Transform ChairPoint;

    public Transform ExitPoint;

    public List<Transform> DisplayPoints;

    public CounterTable CounterTable;

    public EatingSpace EatingSpace;

    private Dictionary<Transform, bool> _displayPoints = new();

    public int MaxCount = 4;

    public int Interval = 1;

    public int CurrentCount { get; set; }

    public bool IsEating;

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

        CurrentCount++;

        yield return new WaitUntil(() => CurrentCount < MaxCount);

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
                        guest.TargetDisplayPoint = GetEmptyDisplay();
                        return guest.TargetDisplayPoint;
                    }
                    else if (guest.IsEating)
                    {
                        LeaveDisplayPoint(guest.TargetDisplayPoint);

                        guest.CurrentMovePoint = GuestMovePoints.EatingWaitPoint;
                        return EatingWaitPoint;
                    }
                    else
                    {
                        LeaveDisplayPoint(guest.TargetDisplayPoint);

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
                    guest.CurrentMovePoint = GuestMovePoints.CounterReturn;
                    return CounterReturnPoint;
                }
            case GuestMovePoints.CounterReturn:
            case GuestMovePoints.EatingTable:
                {
                    guest.CurrentMovePoint = GuestMovePoints.Exit;
                    return ExitPoint;
                }
            case GuestMovePoints.EatingWaitPoint:
                {
                    guest.CurrentMovePoint = GuestMovePoints.EatingTable;
                    return EatingSpacePoint;
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

    private void LeaveDisplayPoint(Transform displayPoint)
    {
        _displayPoints[displayPoint] = false;
    }
}