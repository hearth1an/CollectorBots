using System;
using UnityEngine;

public class ItemSocket : MonoBehaviour
{
    [SerializeField] private Transform _socket;

    public Plant _currentPlant = null;
    private Vector3 _plantSize = new Vector3(10, 10, 10);

    public event Action PlantTaken;
    public event Action PlantDumped;    

    public bool IsOccupied { get; private set; } = false;

    public void Collect(Plant plant)
    {
        plant.transform.parent = _socket.transform.parent;
        plant.transform.position = _socket.transform.position;        

        _currentPlant = plant;
        _currentPlant.transform.localScale = _plantSize;
        
        IsOccupied = true;

        PlantTaken?.Invoke();
    }

    public void Dump()
    {        
        PlantDumped?.Invoke();

        Destroy(_currentPlant.gameObject);

        IsOccupied = false;        
    }
}
