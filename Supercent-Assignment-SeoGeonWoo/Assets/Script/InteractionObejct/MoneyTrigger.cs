using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTrigger : InteractionObejct
{
    public int Money;

    public List<Money> Moneies = new();

    public float DeactiveSpeed = 1;

    public bool IsOperating;

    private float _lineYCount = 0;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (false == IsOperating)
            {
                if (Moneies.Count > 0)
                {
                    Operate();
                }
            }
        }
    }

    public void DropMoney()
    {
        int count = 1;

        for (int i = 0; i < 9; i++)
        {
            Money money = ObjectPoolManager.Instance.SpawnMoney();

            Moneies.Add(money);

            money.transform.rotation = Quaternion.Euler(0, 90, 0);

            if (count == 3)
            {
                count = 0;
            }

            if (i < 3)
            {
                money.transform.position = new Vector3(transform.position.x + (count * 0.8f), transform.position.y + _lineYCount, transform.position.z);
            }
            else if (i < 6)
            {
                money.transform.position = new Vector3(transform.position.x + (count * 0.8f), transform.position.y + _lineYCount, transform.position.z + 0.4f);
            }
            else
            {
                money.transform.position = new Vector3(transform.position.x + (count * 0.8f), transform.position.y + _lineYCount, transform.position.z + 0.8f);
            }

            count++;
        }
        _lineYCount += 0.2f;
    }

    public override void Operate()
    {
        IsOperating = true;

        GetMoney();

        int count = Moneies.Count;

        StartCoroutine(ProcessClearMoney(count));
    }

    private void GetMoney()
    {
        GameData.Instance.AddMoney(Money);
    }

    private IEnumerator ProcessClearMoney(int count)
    {
        for (int i = count - 1; i >= 0; i--)
        {
            while (Moneies[i].transform.position.y < 3)
            {
                Moneies[i].transform.position += Vector3.up * DeactiveSpeed * Time.deltaTime;
                yield return null;
            }

            ObjectPoolManager.Instance.Despawn(Moneies[i]);
        }

        Moneies.Clear();
        Money = 0;
        IsOperating = false;
    }
}