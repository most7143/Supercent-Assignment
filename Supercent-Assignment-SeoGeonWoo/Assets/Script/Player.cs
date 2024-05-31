using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick Joystick;

    public Rigidbody Rigid;

    public Animator Animator;

    public float MoveSpeed = 1;

    private bool _isMove = false;

    private Vector2 _direction;

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

    private void FixedUpdate()
    {
        if (_isMove)
        {
            _direction = Joystick.GetStickDir().normalized;

            Rigid.velocity = new Vector3(_direction.x, 0, _direction.y) * MoveSpeed;

            Look();
        }
    }

    private void Look()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.y));

        transform.rotation = rot;
    }
}