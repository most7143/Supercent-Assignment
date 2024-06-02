using UnityEngine;

public partial class Player
{
    public PlayerStates State = PlayerStates.Default;

    public void CarryOn()
    {
        Animator.SetBool("IsStack", true);
        IsStack = true;
    }

    public void CarryOff()
    {
        Animator.SetBool("IsStack", false);
        IsStack = false;
    }
}