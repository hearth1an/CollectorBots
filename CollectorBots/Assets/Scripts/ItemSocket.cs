using System;
using UnityEngine;

public class ItemSocket : Collector
{
    [SerializeField] private Transform _socket;

    private Counter _counter => Base.DumpPlace.Counter;
    private Plant _currentPlant = null;
    
    public event Action PlantTaken;
    public event Action PlantDumped;

    public bool IsOccupied { get; private set; } = false;

    private void Start()
    {
        //_counter = Base.DumpPlace.Counter;        
    }

    public void Collect(Plant plant)
    {
        plant.transform.parent = _socket.transform.parent;
        plant.transform.position = _socket.transform.position;        

        _currentPlant = plant;
        _currentPlant.transform.localScale = new Vector3(10, 10, 10);
        
        IsOccupied = true;

        PlantTaken?.Invoke();
    }

    public void Dump()
    {        
        PlantDumped?.Invoke();

        Destroy(_currentPlant.gameObject);

        _counter.Add();

        IsOccupied = false;        
    }
}
