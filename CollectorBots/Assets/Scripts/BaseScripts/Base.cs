using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private CollectorsSpawner _collectorsSpawner;
    [SerializeField] private DumpPlace _dumpPlace;
    [SerializeField] private Target _target;

    public DumpPlace DumpPlace => _dumpPlace;
    public Scanner Scanner => _scanner;

    private void Start()
    {
        _scanner.ResourcesDetected += AssignResourcesToBots;
        _dumpPlace.BasePriceCollected += AssignBuildBase;
    }

    private void OnDestroy()
    {
        _scanner.ResourcesDetected -= AssignResourcesToBots;
        _dumpPlace.BasePriceCollected -= AssignBuildBase;
    }

    public bool IsFlagPlaced()
    {
        return _target.IsFlagPlaced;
    }

    private void AssignBuildBase()
    {
        int collectorIndex = 0;

        if (_target.IsFlagPlaced)
        {
            Collector collector = _collectorsSpawner.CreatedObjects[collectorIndex];

            collector.SetBuildingTarget(_target.GetFlagPosition());

            _collectorsSpawner.SetStartSpawnedCount();
            _collectorsSpawner.AssignCollector(collector);

            collector.gameObject.name = "Builder";
        }        
    }

    private void AssignResourcesToBots(Plant[] plants)
    {
        if (plants.Length == 0) return;        

        foreach (Collector collector in _collectorsSpawner.CreatedObjects)
        {
            if (!collector.IsBusy)
            {
                foreach (Plant plant in plants)
                {
                    if (plant.IsScanned == false)
                    {
                        plant.MarkAsScanned();
                        collector.SetTarget(plant);
                        break;
                    }
                }
            }
        }
    }
}
