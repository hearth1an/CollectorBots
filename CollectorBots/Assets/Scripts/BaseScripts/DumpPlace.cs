using System;
using UnityEngine;

public class DumpPlace : MonoBehaviour
{
    [SerializeField] private ResourcesCounter _counter;

    public event Action CollectorPriceCollected;
    public event Action BasePriceCollected;

    private bool _isHaveToPay = false;

    public void UpdateCounter()
    {
        _counter.Add();

        if (_counter.HaveResourcesForCollector() && _isHaveToPay)
        {
            CollectorPriceCollected?.Invoke();
            _counter.PayForCollector();
        }

        if (_counter.HaveResourcesForBase())
        {
            BasePriceCollected?.Invoke();
        }
    }

    public void AllowPayment()
    {
        _isHaveToPay = true;    
    }

    public void CancelPayment()
    {
        _isHaveToPay = false;
    }
}