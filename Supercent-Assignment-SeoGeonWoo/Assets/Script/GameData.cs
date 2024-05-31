using UnityEngine;

public class GameData : MonoBehaviour
{
    public int Gold;

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

    public void AddGold(int gold)
    {
        Gold += gold;
    }

    public bool TryUseGold(int gold)
    {
        return Gold >= gold;
    }

    public void UseGold(int gold)
    {
        Gold -= gold;
    }
}