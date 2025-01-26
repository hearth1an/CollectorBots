using System;
using UnityEngine;

public class ResourcesCounter : MonoBehaviour
{
    [SerializeField] private int _collectorPrice = 3;
    [SerializeField] private int _basePrice = 5;

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

    public bool HaveResourcesForBase()
    {
        if (Value >= _basePrice)
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
