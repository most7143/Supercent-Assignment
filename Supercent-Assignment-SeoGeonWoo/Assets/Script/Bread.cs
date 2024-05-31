using UnityEngine;

public enum ObejectStates
{
    None,
    InBox,
    OnHand,
}

public class Bread : MonoBehaviour
{
    public int SID;
    public ObejectStates State;
    public bool IsActivate;

    public Rigidbody Rigid;

    public void Init(int sid)
    {
        SID = sid;
        Deactivate();
    }

    public void Activate()
    {
        IsActivate = true;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        IsActivate = false;
        gameObject.SetActive(false);
        State = ObejectStates.None;
    }
}