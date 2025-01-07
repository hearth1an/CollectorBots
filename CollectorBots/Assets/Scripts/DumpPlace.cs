using UnityEngine;

public class DumpPlace : MonoBehaviour
{
    [SerializeField] private Counter _counter;

    public void UpdateCounter()
    {
        _counter.Add();
    }
}
