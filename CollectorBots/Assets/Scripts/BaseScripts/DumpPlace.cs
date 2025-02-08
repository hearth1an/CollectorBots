using System;
using UnityEngine;

public class DumpPlace : MonoBehaviour
{
    [SerializeField] private ResourcesCounter _counter;

    public event Action CollectorPriceCollected;
    public event Action BasePriceCollected;

    private bool _isHaveToPay;

    public void UpdateCounter()
    {
        _counter.Add();

        if (_counter.HaveResourcesForCollector())
        {
            CollectorPriceCollected?.Invoke();

            if (_isHaveToPay)
            {
                _counter.PayForCollector();
            }            
        }

        if (_counter.HaveResourcesForBase())
        {
            BasePriceCollected?.Invoke();

            if (_isHaveToPay)
            {
                _counter.PayForBase();
            }            
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