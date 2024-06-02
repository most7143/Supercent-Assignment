using UnityEngine;

public class Guest : Character
{
    public Transform TargetPoint;
    public bool IsActivate;
    public Vector3 Offset;

    public void Update()
    {
        if (IsActivate)
        {
            Move();

            RayCast();
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
    }

    public void Move()
    {
        if (TargetPoint == null)
            return;

        if (false == _isMove)
        {
            return;
        }

        _direction = TargetPoint.position - transform.position;

        Debug.Log(_direction);

        transform.rotation = Quaternion.LookRotation(_direction).normalized;

        transform.position += _direction.normalized * MoveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, TargetPoint.position) < 1)
        {
            MoveOff();
        }
    }

    public void SetTarget(Transform target)
    {
        TargetPoint = target;
    }

    private void RayCast()
    {
        Debug.DrawRay(transform.position + Offset, transform.forward * 0.2f, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(transform.position + Offset, transform.forward, out hit, 0.2f))
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Guest"))
            {
                Debug.Log(hit.collider.name);
                MoveOff();
            }
        }
        else
        {
            MoveOn();
        }
    }
}