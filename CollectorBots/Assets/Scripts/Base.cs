using System.Collections;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private CollectorsSpawner _unitSpawner;
    [SerializeField] private PlantSpawner _plantsSpawner;
    [SerializeField] private ScannerAnimController _scanner;
    [SerializeField] private DumpPlace _dumpPlace;

    private float _taskDelay = 0.1f;
    private Coroutine _taskRoutine;

    public ScannerAnimController Scanner => _scanner;
    public DumpPlace DumpPlace => _dumpPlace;

    private void Awake()
    {
        

        _scanner.AreaScanned += TryGiveTasks;
    }

    private void OnDestroy()
    {
        _scanner.AreaScanned -= TryGiveTasks;
    }

    private IEnumerator TaskRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(_taskDelay);

        while (enabled)
        {
            GiveTasks();
            yield return delay;

            if (_unitSpawner.HasAvailableCollectors() || _plantsSpawner.AreUncollectedPlants())
            {
                _taskRoutine = null;
                yield break;
            }

        }
    }

    private void TryGiveTasks()
    {
        if (_unitSpawner.HasAvailableCollectors() && _plantsSpawner.AreUncollectedPlants())
        {
            if (_taskRoutine == null)
            {
                _taskRoutine = StartCoroutine(TaskRoutine());
            }
        }
    }

    private void GiveTasks()
    {
        foreach (Collector collector in _unitSpawner.CreatedObjects)
        {
            if (collector.IsDoingTask() == false)
            {
                Plant plant = _plantsSpawner.GetRandomPlant();

                if (plant != null)
                {
                    collector.SetTarget(plant);
                }
            }
        }
    }
}
