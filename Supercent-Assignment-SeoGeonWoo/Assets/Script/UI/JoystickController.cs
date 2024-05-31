using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public Joystick Joystick;

    public Player Player;

    public void Awake()
    {
        if (Joystick == null)
        {
            Joystick = GetComponentInChildren<Joystick>();
        }

        OffJoystick();
    }

    public void OnJoystick()
    {
        Joystick.RestPosition();
        Joystick.IsActivate = true;
        Joystick.gameObject.SetActive(true);
    }

    public void OffJoystick()
    {
        Joystick.gameObject.SetActive(false);
        Joystick.IsActivate = false;
    }

    public void Update()
    {
        if (false == GameManager.Instance.IsMoveLock)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnJoystick();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OffJoystick();
            }

            if (IsMoveJoystick())
            {
                Player.MoveOn();
            }
            else
            {
                Player.MoveOff();
            }
        }
    }

    private bool IsMoveJoystick()
    {
        return Joystick.IsActivate && Joystick.GetStickDir() != Vector3.zero;
    }
}