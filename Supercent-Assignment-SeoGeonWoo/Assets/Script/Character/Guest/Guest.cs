using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Guest : Character
{
    public GuestController Controller;

    public Vector3 Offset;

    public int MovePointCount;

    public Vector3 UIOffset;

    public RectTransform UIRect;

    public Transform UINeedObject;

    public Image UIPayIcon;

    public TextMeshProUGUI UINeedCountText;

    public bool IsActivate { get; set; }

    public bool IsArrive;
    public Transform TargetPoint { get; set; }
    public Transform LookPoint { get; set; }

    public GuestMovePoints CurrentMovePoint { get; set; }

    public int MaxTakeBreadCount;

    public void Update()
    {
        Rigid.velocity = Vector3.zero;
        Rigid.angularVelocity = Vector3.zero;

        if (Moveable())
        {
            Move();
        }

        FollowUI();
    }

    private void FollowUI()
    {
        UIRect.transform.position = Camera.main.WorldToScreenPoint(transform.position + UIOffset);

        if (IsArrive && CurrentMovePoint == GuestMovePoints.DisplayTable)
        {
            UIPayIcon.gameObject.SetActive(false);

            if (false == UIRect.gameObject.activeInHierarchy)
            {
                UIRect.gameObject.SetActive(true);
            }

            RefreshText();
        }
        else if (Breads.Count > 0)
        {
            if (false == UIPayIcon.gameObject.activeInHierarchy)
            {
                UIPayIcon.gameObject.SetActive(true);
            }
        }
        else
        {
            if (UIRect.gameObject.activeInHierarchy)
            {
                UIRect.gameObject.SetActive(false);
            }
        }
    }

    public void Init()
    {
        Deactivate();
    }

    public void Activate()
    {
        IsActivate = true;
        _isMove = true;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        IsActivate = false;
        gameObject.SetActive(false);
        CurrentMovePoint = GuestMovePoints.Center;
        MovePointCount = 0;
    }

    public void Move()
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
            Look(Controller.DisplayTable);

            StartCoroutine(ProcessTakeBread());
        }
        else if (CurrentMovePoint == GuestMovePoints.Counter)
        {
            StartCoroutine(ProcessPayMoneyByBread());
        }
        else if (CurrentMovePoint == GuestMovePoints.Exit)
        {
            ObjectPoolManager.Instance.Despawn(this);
        }
        else
        {
            NextPoint();
        }
    }

    private IEnumerator ProcessTakeBread()
    {
        int rand = Random.Range(3, 6);

        MaxTakeBreadCount = rand;

        yield return new WaitUntil(() => MaxTakeBreadCount == CurrentTakeCount);

        IsArrive = false;
        NextPoint();
    }

    private IEnumerator ProcessPayMoneyByBread()
    {
        yield return new WaitUntil(() => Controller.CounterTable.IsOperating);

        int money = Breads[0].Price * CurrentTakeCount;

        Controller.CounterTable.MoneyTrigger.Money += money;

        for (int i = Breads.Count - 1; i >= 0; i--)
        {
            ObjectPoolManager.Instance.Despawn(Breads[i]);
            yield return new WaitForSeconds(0.3f);
        }

        IsArrive = false;

        Breads.Clear();

        StartCoroutine(Controller.CounterTable.ProcessClose(this));

        yield return new WaitUntil(() => Controller.CounterTable.IsCloseBag);
        yield return new WaitUntil(() => false == Controller.CounterTable.MoneyTrigger.IsOperating);

        Controller.CounterTable.MoneyTrigger.DropMoney();

        NextPoint();
    }

    private void Look(Transform lookPoint)
    {
        LookPoint = lookPoint;

        Vector3 lookDirection = LookPoint.position - transform.position;

        transform.rotation = Quaternion.LookRotation(lookDirection).normalized;
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

    public void SetTarget(Transform target)
    {
        TargetPoint = target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Guest"))
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.forward, 5f);

            for (int i = 0; i < hits.Length; i++)
            {
                Debug.Log(hits[i].collider.name);

                if (hits[i].collider.CompareTag("Guest"))
                {
                    MoveOff();
                }
                else if (hits[i].collider.CompareTag("Player"))
                {
                    MoveOff();
                    return;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Guest"))
        {
            if (false == IsArrive)
            {
                MoveOn();
            }
        }
    }

    public void RefreshText()
    {
        UINeedObject.gameObject.SetActive(true);
        UINeedCountText.SetText((MaxTakeBreadCount - CurrentTakeCount).ToString());
    }
}