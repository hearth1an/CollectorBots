using UnityEngine;

public class DumpPlace : MonoBehaviour
{
    [SerializeField] private ResourcesCounter _counter;

    public void UpdateCounter()
    {
        _counter.Add();
    }
}
