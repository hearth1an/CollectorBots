using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private PlantSpawner _plantsSpawner;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Button _button;  

    private void Awake()
    {
        _button.onClick.AddListener(GiveTask);
        _scanner.AreaScanned += AddPlants;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(GiveTask);
        _scanner.AreaScanned -= AddPlants;
    }

    private void AddPlants()
    {
        
        
    }

    private void GiveTask()
    {
        int spawnedObjects = _plantsSpawner.CreatedObjects.Count;

        Debug.Log(_plantsSpawner.CreatedObjects.Count);

        foreach (Collector collector in _unitSpawner.CreatedObjects)
        {
            Plant plant = _plantsSpawner.GetRandomPlant();            

            if (collector.IsBusy == false)
            {
                collector.SetCollectTarget(plant);
                _plantsSpawner.CreatedObjects.Remove(plant);
            }
            
        }       
    }
}
