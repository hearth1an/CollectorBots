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

        Debug.Log(_unitSpawner.CreatedObjects.Count);        

        for (int i = 0; i < _unitSpawner.CreatedObjects.Count+1; i++)
        {
            Plant plant = GetRandomTarget();
            Collector collector = _unitSpawner.CreatedObjects[i];
            collector.SetCollectTarget(plant);

            collector.DumpedOk += RemovePlant;

           // _plantsSpawner.CreatedObjects.Remove(plant);
        }
    }

    private void RemovePlant(Plant plant)
    {
        _plantsSpawner.CreatedObjects.Remove(plant);
    }

    private Plant GetRandomTarget()
    {
        int targetIndex = Random.RandomRange(0, _plantsSpawner.CreatedObjects.Count + 1);        

        return _plantsSpawner.CreatedObjects[targetIndex];
    }


}
