using System.Collections;
using UnityEngine;

public class OvenBasket : InteractionObejct
{
    public BreadOven Oven;

    public Player Player
    { get { return GameManager.Instance.Player; } }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (Oven.TryCarry())
            {
                Operate();
            }
        }
    }

    public override void Operate()
    {
        TakeToPlayer();
    }

    private void TakeToPlayer()
    {
        GameManager.Instance.LockMove();
        Player.CarryOn();
        StartCoroutine(ProcessTake());
    }

    private IEnumerator ProcessTake()
    {
        int count = Oven.CurrentCount;

        for (int i = 0; i < count; i++)
        {
            if (Oven.MaxCount == Player.CurrentTakeCount)
            {
                break;
            }

            int sid = ObjectPoolManager.Instance.FindSID(ObjectStates.InBreadBox);
            ObjectPoolManager.Instance.Despawn(sid);
            SpawnToPlayerHand();
            Oven.CurrentCount--;
            yield return new WaitForSeconds(0.2f);
        }

        GameManager.Instance.UnlockMove();
    }

    private void SpawnToPlayerHand()
    {
        Bread bread = ObjectPoolManager.Instance.Spawn(ObjectStates.OnHand);

        if (bread != null)
        {
            bread.Rigid.velocity = Vector3.zero;
            bread.transform.position = new Vector3(Player.TakePoint.position.x, Player.GetTakeY(), Player.TakePoint.position.z);
            bread.transform.rotation = Quaternion.identity;
            bread.HoldOn();
            bread.transform.parent = Player.transform;
            Player.AddBread(bread);
        }
    }
}