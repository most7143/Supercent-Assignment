using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestController : MonoBehaviour
{
    public Transform SpawnPoint;

    public Transform CenterPoint;

    public List<Transform> DisplayPoints;

    public List<Transform> CounterPoints;

    public int MaxCount = 4;

    public int Interval = 1;

    private int _currentCount;

    private void Start()
    {
        StartCoroutine(ProcessSpawn());
    }

    private IEnumerator ProcessSpawn()
    {
        yield return new WaitForSeconds(Interval);

        Guest guest = ObjectPoolManager.Instance.SpawnGuest();

        guest.SetTarget(CenterPoint);

        guest.transform.position = SpawnPoint.transform.position;

        _currentCount++;

        yield return new WaitUntil(() => _currentCount < MaxCount);

        StartCoroutine(ProcessSpawn());
    }
}