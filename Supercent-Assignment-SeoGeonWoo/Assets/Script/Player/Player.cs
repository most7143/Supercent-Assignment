using UnityEngine;

public enum PlayerStates
{
    Default,
    Carry,
}

public partial class Player : MonoBehaviour
{
    public Joystick Joystick;

    public Rigidbody Rigid;

    public float MoveSpeed = 1;

    private Vector2 _direction;

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