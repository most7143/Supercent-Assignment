using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager _instance;

    public Dictionary<int, Bread> Breads = new();

    public GameObject Bread;

    public static ObjectPoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ObjectPoolManager();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        Init();
    }

    private void Init()
    {
        for (int i = 0; i < 20; i++)
        {
            Bread bread = Instantiate(Bread, Vector3.zero, Quaternion.identity).GetComponent<Bread>();

            Breads.Add(i, bread);
            bread.Init(i);
        }
    }

    public Bread Spawn()
    {
        for (int i = 0; i <= Breads.Count; i++)
        {
            if (false == Breads[i].IsActivate)
            {
                Breads[i].Activate();
                return Breads[i];
            }
        }

        return null;
    }

    public void Despawn(int sid)
    {
        Breads[sid].Deactivate();
    }
}