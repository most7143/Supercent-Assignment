using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour
{
    public Joystick Joystick;

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
        Joystick.gameObject.SetActive(true);

        Joystick.Rect.anchoredPosition3D = Input.mousePosition;
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
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OffJoystick();
        }
    }
}