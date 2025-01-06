using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private CollectorsSpawner _unitSpawner;
    [SerializeField] private PlantSpawner _plantsSpawner;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private DumpPlace _dumpPlace;

    public Scanner Scanner { get; private set; }
    public DumpPlace DumpPlace { get; private set; }

    private void Awake()
    {    
        Scanner = _scanner;
        DumpPlace = _dumpPlace;

        _scanner.AreaScanned += GiveTask;
    }

    private void OnDisable()
    {
        _scanner.AreaScanned -= GiveTask;
    }

    private void GiveTask()
    {
        foreach (Collector collector in _unitSpawner.CreatedObjects)
        {
            Plant plant = _plantsSpawner.GetRandomPlant();            

            if (collector.IsBusy == false && plant != null)
            {
                collector.SetTarget(plant);
                _plantsSpawner.CreatedObjects.Remove(plant);
            }
            else
            {
                return;
            }
        }       
    }
}
