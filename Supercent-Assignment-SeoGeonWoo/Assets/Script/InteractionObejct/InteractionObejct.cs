using UnityEngine;

public abstract class InteractionObejct : MonoBehaviour
{
    public Collider Collider;

    public abstract void Operate();
}