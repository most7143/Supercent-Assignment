using System.Collections;
using UnityEngine;

public class BreadOven : InteractionObejct
{
    public Transform SpawnPoint;

    public float Interval;

    public int MaxCount;

    public float Power;

    public int CurrentCount { get; set; }

    public void Start()
    {
        Operate();
    }

    public override void Operate()
    {
        StartCoroutine(ProcessSpawn());
    }

    private IEnumerator ProcessSpawn()
    {
        yield return new WaitForSeconds(Interval);

        Bread bread = ObjectPoolManager.Instance.SpawnBread();

        bread.transform.position = SpawnPoint.transform.position;

        yield return new WaitForSeconds(Interval * 0.5f);

        SpawnForce(bread);

        bread.State = ObjectStates.InBreadBox;

        CurrentCount++;

        yield return new WaitUntil(() => CurrentCount < MaxCount);

        StartCoroutine(ProcessSpawn());
    }

    private void SpawnForce(Bread bread)
    {
        float rand = Random.Range(-0.1f, 0.1f);

        Vector3 dir = new Vector3(transform.forward.x + rand, transform.forward.y, transform.forward.z);

        bread.Rigid.AddForce(dir * Power, ForceMode.Impulse);
    }

    public bool TryCarry()
    {
        if (CurrentCount > 0)
        {
            if (GameManager.Instance.Player.CurrentTakeCount < MaxCount)
            {
                return true;
            }
        }

        return false;
    }
}