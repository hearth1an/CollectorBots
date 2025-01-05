using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private Collector _collector;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Button _button;
    [SerializeField] private RandomSpawner _plantsSpawner;

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
        _collector.SetCollectTarget(_plantsSpawner.CreatedObjects[0]);
    }



}
