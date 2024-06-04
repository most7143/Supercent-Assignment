using UnityEngine;

public class CounterTrigger : MonoBehaviour
{
    public CounterTable Table;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Table.TryOperate())
            {
                Table.Operate();
            }
        }
    }
}