using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ItemSocket : MonoBehaviour
{
    [SerializeField] private Transform _container;

    private Counter _counter;

    public Plant _currentPlant;
    public bool IsOccupied { get; private set; } = false;

    public event Action PlantCollected;
    public event Action PlantDumped;

    private void Awake()
    {
        _counter = FindObjectOfType<Counter>();
    }
    public void Collect(Plant plant)
    {
        plant.transform.parent = _container.transform.parent;
        plant.transform.position = _container.transform.position;
        plant.transform.localScale = new Vector3(10, 10, 10);

        _currentPlant = plant;
       // _currentPlant.transform.localScale = new Vector3(10, 10, 10);

        IsOccupied = true;

        PlantCollected?.Invoke();
    }

    public void Dump()
    {
        Destroy(_currentPlant.gameObject);
        PlantDumped?.Invoke();
        _counter.UpdateCount();
        IsOccupied = false;
        
    }
}
