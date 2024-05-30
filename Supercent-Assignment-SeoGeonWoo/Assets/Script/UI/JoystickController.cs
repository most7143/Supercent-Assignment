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
        Joystick.gameObject.SetActive(true);
    }

    public void OffJoystick()
    {
        Joystick.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnJoystick();
            Player.MoveOn();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OffJoystick();
            Player.MoveOff();
        }
    }
}