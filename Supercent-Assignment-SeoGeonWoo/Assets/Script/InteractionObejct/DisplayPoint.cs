using UnityEngine;

public class DisplayPoint : MonoBehaviour
{
    public bool IsActivate = false;

    public Bread Bread;

    public void Activate(Bread bread)
    {
        IsActivate = true;
        Bread = bread;
    }

    public void Deactivate()
    {
        IsActivate = false;
        Bread = null;
    }
}