using System;
using System.Collections;
using UnityEngine;

public class CollectorTaskHandler : Collector
{
    private float _taskDelay = 1f;
    private WaitForSeconds _delay;

    public event Action Collected;

    public bool IsBusy { get; private set; } = false;

    private void Awake()
    {
        _delay = new WaitForSeconds(_taskDelay);
    }

    public void TaskCollect(Plant plant)
    {
        IsBusy = true;
        StartCoroutine(CollectRoutine(plant));
    }

    public void TaskDump()
    {
        StartCoroutine(DumpRoutine());
    }

    private IEnumerator CollectRoutine(Plant plant)
    {
        while (enabled && Socket.IsOccupied == false)
        {
            if (Mover.IsPathEnding())
            {
                Socket.Collect(plant);
                //Collected?.Invoke(plant);
            }

            yield return _delay;
        }
    }

    private IEnumerator DumpRoutine()
    {
        while (enabled && Socket.IsOccupied)
        {
            if (Mover.IsPathEnding())
            {
                Socket.Dump();
                IsBusy = false;
                Mover.GoChill();
            }

            yield return _delay;
        }
    }


}
