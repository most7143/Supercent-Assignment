using UnityEngine;

public partial class Player
{
    public Animator Animator;

    public PlayerStates State = PlayerStates.Default;
    public bool IsStack { get; set; }

    private bool _isMove = false;

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
    }

    public void CarryOff()
    {
        Animator.SetBool("IsStack", false);
    }
}