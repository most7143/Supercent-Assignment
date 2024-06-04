using UnityEngine;

public class Money : MonoBehaviour
{
    public bool IsActivate;

    public void Init()
    {
        Deactivate();
    }

    public void Activate()
    {
        IsActivate = true;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        IsActivate = false;
    }
}