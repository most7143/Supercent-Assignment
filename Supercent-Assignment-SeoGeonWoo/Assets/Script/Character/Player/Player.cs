using UnityEngine;

public enum PlayerStates
{
    Default,
    Carry,
}

public partial class Player : Character
{
    public Joystick Joystick;

    private void FixedUpdate()
    {
        if (_isMove)
        {
            _direction = Joystick.GetStickDir().normalized;

            Rigid.velocity = new Vector3(_direction.x, 0, _direction.y) * MoveSpeed;

            _handler.Look(transform, _direction);
        }
    }
}