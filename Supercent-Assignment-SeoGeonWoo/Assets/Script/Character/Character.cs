using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator Animator;

    public Rigidbody Rigid;

    public float MoveSpeed;
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
}