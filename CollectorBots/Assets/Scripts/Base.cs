using System.Collections;
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

        StartCoroutine(TaskRoutine());
    }

    private IEnumerator TaskRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        while (enabled)
        {
            GiveTasks();
            yield return delay;
        }
    }

    private void GiveTasks()
    {
        //Debug.Log("Task");

        foreach (Collector collector in _unitSpawner.CreatedObjects)
        {
            Plant plant = _plantsSpawner.GetRandomPlant();

            //Debug.Log(collector.IsBusy);

            if (collector.IsBusy == false && plant != null)
            {
                collector.SetTarget(plant);                
            }
            else
            {
                return;
            }
        }       
    }
}
