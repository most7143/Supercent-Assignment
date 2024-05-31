using System.Collections;
using UnityEngine;

public class BreadOven : InteractionObejct
{
    public Transform SpawnPoint;

    public float Interval;

    public int MaxCount;

    public float Power;

    private int currentCount;

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
        while (currentCount < MaxCount)
        {
            yield return new WaitForSeconds(Interval);

            Bread bread = ObjectPoolManager.Instance.Spawn();

            bread.transform.position = SpawnPoint.transform.position;

            yield return new WaitForSeconds(Interval * 0.5f);

            SpawnForce(bread);

            currentCount++;
        }
    }

    private void SpawnForce(Bread bread)
    {
        float rand = Random.Range(-0.2f, 0.2f);

        Vector3 dir = new Vector3(transform.forward.x + rand, transform.forward.y, transform.forward.z);

        bread.Rigid.AddForce(dir * Power, ForceMode.Impulse);
    }
}