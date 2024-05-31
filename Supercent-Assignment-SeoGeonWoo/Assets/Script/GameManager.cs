using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player Player;

    public JoystickController JoystickController;

    private static GameManager _instance;

    public bool IsMoveLock { get; set; }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }
    }

    public void Awake()
    {
        _instance = this;
    }

    public void LockMove()
    {
        JoystickController.OffJoystick();
        IsMoveLock = true;
        Player.MoveOff();
    }

    public void UnlockMove()
    {
        IsMoveLock = false;
    }
}