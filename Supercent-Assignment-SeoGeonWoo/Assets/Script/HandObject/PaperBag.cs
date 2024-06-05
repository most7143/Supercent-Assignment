using UnityEngine;

public class PaperBag : MonoBehaviour
{
    public Animator Animator;

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

    public void Close()
    {
        Animator.SetTrigger("Close");
    }
}