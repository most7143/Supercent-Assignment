using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick Joystick;

    public Rigidbody Rigid;

    public float MoveSpeed = 1;

    private bool isMove = false;

    private Vector2 direction;

    public void MoveOn()
    {
        isMove = true;
    }

    public void MoveOff()
    {
        isMove = false;
        direction = Vector2.zero;
        Rigid.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            direction = Joystick.GetStickDir().normalized;

            Rigid.velocity = new Vector3(direction.x, 0, direction.y) * MoveSpeed;
        }
    }
}