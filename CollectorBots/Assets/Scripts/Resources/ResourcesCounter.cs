using System;
using UnityEngine;

public class ResourcesCounter : MonoBehaviour
{
    [SerializeField] private int _collectorPrice = 3;

    public event Action<int> CountUpdated;

    public int Value { get; private set; } = 0;

    public void Add()
    {
        Value++;
        CountUpdated?.Invoke(Value);
    }

    public bool HaveResourcesForCollector()
    {
        if (Value == _collectorPrice)
        {
            return true;
        }            

        return false;
    }

    public void PayForCollector()
    {
        Value -= _collectorPrice;
        CountUpdated?.Invoke(Value);
    }
}
