using UnityEngine;

public enum ObjectStates
{
    None,
    InBreadBox,
    InBreadBag,
    OnHand,
}

public class Bread : MonoBehaviour
{
    public int SID;
    public ObjectStates State;
    public bool IsActivate;

    public Rigidbody Rigid;
    public BoxCollider Collider;

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
        State = ObjectStates.None;
    }

    public void HoldOn()
    {
        Collider.isTrigger = true;
        Rigid.isKinematic = true;
    }

    public void HoldOff()
    {
        Collider.isTrigger = false;
        Rigid.isKinematic = false;
    }
}