using System.Collections;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private CollectorsSpawner _unitSpawner;
    [SerializeField] private PlantSpawner _plantsSpawner;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private DumpPlace _dumpPlace;

    private Coroutine _taskRoutine;

    public Scanner Scanner => _scanner;
    public DumpPlace DumpPlace => _dumpPlace;

    

    private void Awake()
    {
        //StartCoroutine(TaskRoutine());
        _scanner.AreaScanned += TryGiveTasks;
    }

    private IEnumerator TaskRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        while (enabled)
        {
            GiveTasks();
            yield return delay;

            if (_unitSpawner.FreeCollectors == 0 || _plantsSpawner.ScannedCount == 0)
            {
                _taskRoutine = null;
                yield break;
            }

        }
    }

    private void TryGiveTasks()
    {
        Debug.Log(_unitSpawner.FreeCollectors + " " + _plantsSpawner.ScannedCount);

        if (_unitSpawner.FreeCollectors > 0 && _plantsSpawner.ScannedCount > 0)
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
            if (collector.IsBusy == false)
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
