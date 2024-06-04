using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator Animator;

    public Rigidbody Rigid;

    public float MoveSpeed;

    public Transform TakePoint;

    public List<Bread> Breads;
    public int CurrentTakeCount { get; set; }

    public bool IsStack { get; set; }

    protected bool _isMove = false;

    protected Vector3 _direction;

    protected CharacterHandler _handler = new();

    public void MoveOn()
    {
        _isMove = true;

        Animator.SetBool("IsMove", true);
    }

    public void MoveOff()
    {
        _isMove = false;
        _direction = Vector2.zero;
        Rigid.velocity = Vector3.zero;
        Animator.SetBool("IsMove", false);
    }

    public void CarryOn()
    {
        Animator.SetBool("IsStack", true);
        IsStack = true;
    }

    public void CarryOff()
    {
        Animator.SetBool("IsStack", false);
        IsStack = false;
    }

    public float GetTakeY()
    {
        return TakePoint.position.y + (CurrentTakeCount * 0.5f);
    }

    public void AddBread(Bread bread)
    {
        Breads.Add(bread);
        CurrentTakeCount++;
    }

    public void RemoveBread()
    {
        Bread bread = Breads[Breads.Count - 1];

        Breads.Remove(bread);

        CurrentTakeCount--;

        ObjectPoolManager.Instance.Despawn(bread);
    }

    public void RemoveBreadAll()
    {
        for (int i = 0; i < Breads.Count; i++)
        {
            ObjectPoolManager.Instance.Despawn(Breads[i]);
        }

        CurrentTakeCount = 0;
        Breads.Clear();
    }
}