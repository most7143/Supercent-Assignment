using TMPro;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;

    public int Money;

    private static GameData _instance;

    public static GameData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameData();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void AddMoney(int money)
    {
        Money += money;

        MoneyText.SetText(Money.ToString());
    }

    public bool TryUseMoney(int money)
    {
        return Money >= money;
    }

    public void UseMoney(int money)
    {
        Money -= money;
        MoneyText.SetText(Money.ToString());
    }
}