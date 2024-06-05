using System.Collections;
using UnityEngine;

public partial class Guest
{
    private void FixedUpdate()
    {
        if (Moveable())
        {
            Move();
        }
    }

    private bool Moveable()
    {
        if (false == IsActivate)
        {
            return false;
        }

        if (TargetPoint == null)
            return false;

        if (IsArrive)
            return false;

        if (false == _isMove)
            return false;

        return true;
    }

    private void Move()
    {
        _direction = TargetPoint.position - transform.position;

        MoveOn();

        Look(TargetPoint);

        transform.position += _direction.normalized * MoveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, TargetPoint.position) < 0.2f)
        {
            IsArrive = true;

            MoveOff();

            NextAction();
        }
    }

    private void NextAction()
    {
        if (CurrentMovePoint == GuestMovePoints.DisplayTable)
        {
            Look(Controller.DisplayTable.transform);

            StartCoroutine(ProcessTakeBread());
        }
        else if (CurrentMovePoint == GuestMovePoints.Counter)
        {
            StartCoroutine(ProcessPayMoneyByBread());
        }
        else if (CurrentMovePoint == GuestMovePoints.Exit)
        {
            Controller.CurrentCount--;
            ObjectPoolManager.Instance.Despawn(this);
        }
        else if (CurrentMovePoint == GuestMovePoints.EatingWaitPoint)
        {
            StartCoroutine(ProcessEatingSpaceToGuest());
        }
        else if (CurrentMovePoint == GuestMovePoints.EatingTable)
        {
            StartCoroutine(ProcessEatingGuest());
        }
        else
        {
            NextPoint();
        }
    }

    private IEnumerator ProcessTakeBread()
    {
        int rand = Random.Range(2, 5);

        MaxTakeBreadCount = rand;

        yield return new WaitUntil(() => MaxTakeBreadCount == CurrentTakeCount);

        if (false == Controller.IsEating)
        {
            Controller.IsEating = true;
            IsEating = true;
        }

        IsArrive = false;
        NextPoint();
    }

    private IEnumerator ProcessPayMoneyByBread()
    {
        yield return new WaitUntil(() => Controller.CounterTable.IsOperating);

        int money = GameData.Instance.BreadPrice * CurrentTakeCount;

        Controller.CounterTable.MoneyTrigger.Money += money;

        RemoveBreadAll();

        IsArrive = false;

        StartCoroutine(Controller.CounterTable.ProcessClose(this));

        yield return new WaitUntil(() => Controller.CounterTable.IsCloseBag);
        yield return new WaitUntil(() => false == Controller.CounterTable.MoneyTrigger.IsOperating);

        Controller.CounterTable.MoneyTrigger.DropMoney();

        NextPoint();
    }

    private IEnumerator ProcessEatingSpaceToGuest()
    {
        yield return new WaitUntil(() => GameData.Instance.UsingEatingSpace);

        IsArrive = false;

        NextPoint();
    }

    private IEnumerator ProcessEatingGuest()
    {
        int money = GameData.Instance.BreadPrice * CurrentTakeCount;

        transform.position = Controller.ChairPoint.position;
        Animator.SetBool("IsSitting", true);

        Look(LookPoint);

        Controller.EatingSpace.Seat();
        RemoveBreadAll();
        yield return new WaitForSeconds(2f);
        Animator.SetBool("IsSitting", false);

        IsArrive = false;

        ResetEating();

        Controller.EatingSpace.EatBread(money);
        NextPoint();
    }

    private void ResetEating()
    {
        Controller.IsEating = false;
        IsEating = false;
    }

    private void NextPoint()
    {
        Transform target = Controller.NextPoints(this);

        if (target != null)
        {
            MovePointCount++;

            SetTarget(target);
            MoveOn();
            IsArrive = false;
        }
    }
}