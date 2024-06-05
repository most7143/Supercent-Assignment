using UnityEngine;

public partial class Guest : Character
{
    public GuestController Controller;

    public Vector3 Offset;

    public int MovePointCount;

    public bool IsEating;

    public PaperBag PaperBag;

    public FollowUI FollowUI;

    public bool IsActivate { get; set; }

    public bool IsArrive { get; set; }
    public Transform TargetPoint { get; set; }
    public Transform LookPoint { get; set; }

    public Transform TargetDisplayPoint { get; set; }

    public GuestMovePoints CurrentMovePoint { get; set; }

    public int MaxTakeBreadCount;

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
        FollowUI.UICanvasGroup.alpha = 0;
        IsActivate = false;
        IsArrive = false;
        MaxTakeBreadCount = 0; gameObject.SetActive(false);
        CurrentMovePoint = GuestMovePoints.Center;
        MovePointCount = 0;
        CurrentTakeCount = 0;

        if (PaperBag != null)
        {
            ObjectPoolManager.Instance.Despawn(PaperBag);
            PaperBag = null;
        }

        TargetDisplayPoint = null;
    }

    public void SetTarget(Transform target)
    {
        TargetPoint = target;
    }

    private void Look(Transform lookPoint)
    {
        LookPoint = lookPoint;

        Vector3 lookDirection = LookPoint.position - transform.position;

        transform.rotation = Quaternion.LookRotation(lookDirection).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Guest"))
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.forward, 5f);

            for (int i = 0; i < hits.Length; i++)
            {
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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Guest"))
        {
            if (false == IsArrive)
            {
                MoveOn();
            }
        }
    }
}