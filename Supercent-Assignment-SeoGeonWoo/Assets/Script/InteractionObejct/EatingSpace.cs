using System.Collections;
using TMPro;
using UnityEngine;

public class EatingSpace : MonoBehaviour
{
    public int UnlockMoney = 30;

    public TextMeshPro PriceText;

    public Transform LockSpace;

    public Transform UnlockSpace;

    public GameObject TrashObject;
    public GameObject TrashParticle;
    public GameObject EatingBread;

    public MoneyTrigger MoneyTrigger;

    private void Start()
    {
        PriceText.SetText(UnlockMoney.ToString());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (TrashObject.activeInHierarchy)
            {
                ClearTrash();
            }

            if (UnlockMoney <= 0)
                return;

            if (GameData.Instance.Money >= UnlockMoney)
            {
                StartCoroutine(ProcessUnlock(GameData.Instance.Money));
            }
        }
    }

    private IEnumerator ProcessUnlock(int count)
    {
        if (count > 0)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                Money money = ObjectPoolManager.Instance.SpawnMoney();

                money.transform.position = transform.position;

                while (money.transform.position.y < 3)
                {
                    money.transform.position += Vector3.up * 30f * Time.deltaTime;
                    yield return null;
                }

                ObjectPoolManager.Instance.Despawn(money);

                GameData.Instance.UseMoney(1);
                UnlockMoney--;
                PriceText.SetText(UnlockMoney.ToString());

                if (GameData.Instance.Money <= 0 || UnlockMoney == 0)
                {
                    Unlock();
                    StopAllCoroutines();
                    break;
                }
            }
        }
    }

    private void Unlock()
    {
        UnlockSpace.gameObject.SetActive(true);
        LockSpace.gameObject.SetActive(false);

        GameData.Instance.UsingEatingSpace = true;
    }

    public void Seat()
    {
        EatingBread.SetActive(true);
    }

    public void EatBread(int money)
    {
        EatingBread.SetActive(false);
        TrashObject.SetActive(true);

        MoneyTrigger.Money = money;
        MoneyTrigger.DropMoney();
    }

    public void ClearTrash()
    {
        TrashObject.SetActive(false);
        TrashParticle.SetActive(true);
    }
}