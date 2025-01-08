using System;
using UnityEngine;

public class ResourcesCounter : MonoBehaviour
{   
    public event Action<int> CountUpdated;

    public int Value { get; private set; } = 0;

    public void Add()
    {
        Value++;
        CountUpdated?.Invoke(Value);
    }
}
