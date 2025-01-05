using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Button _button;
    [SerializeField] private PlantSpawner _plantsSpawner;

    private List<Plant> _foundPlants = new List<Plant>();
    private float _scanRadius = 30f;

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
            Plant plant = GetRandomTarget();            

            collector.SetCollectTarget(plant);
            _plantsSpawner.CreatedObjects.Remove(plant);

            //collector.DumpedOk += RemovePlant;
        }       
    }

    private void RemovePlant(Plant plant)
    {
        
        Destroy(plant);
    }

    private Plant GetRandomTarget()
    {
        int targetIndex = Random.Range(0, _plantsSpawner.CreatedObjects.Count);        

        return _plantsSpawner.CreatedObjects[targetIndex];
    }


}
