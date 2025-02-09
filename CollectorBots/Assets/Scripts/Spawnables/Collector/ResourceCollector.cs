using System;
using System.Collections;
using UnityEngine;

public class ResourceCollector
{   
    private ItemSocket _itemSocket;
    private ICoroutineRunner _coroutineRunner;

    private float _taskDelay = 1f;
    private WaitForSeconds _delay;
    private bool _isBusy;
    private bool _isBuilding;

    public bool IsBusy => _isBusy;
    public bool IsBuilding => _isBuilding;

    public event Action<Plant> Collected;
   
    public event Action Dumped;

    public ResourceCollector(ItemSocket itemSocket, ICoroutineRunner coroutineRunner)
    {
        _itemSocket = itemSocket;
        _coroutineRunner = coroutineRunner;
        _delay = new WaitForSeconds(_taskDelay);
    }

    public void SetTarget(Plant plant, CollectorMovement movement)
    {       
        _isBusy = true;
        movement.GoTo(plant.transform.position);
        _coroutineRunner.StartCoroutine(CollectRoutine(plant, movement));
    }

    public void SetBuildTarget(Flag flag, CollectorMovement movement)
    {
        if (flag != null)
        {
            _isBusy = true;
            _isBuilding = true;

            movement.GoTo(flag.transform.position);
            _coroutineRunner.StartCoroutine(BuildRoutine(flag, movement));
        }

        _isBusy = false;
        _isBuilding = false;
    }

    private IEnumerator CollectRoutine(Plant plant, CollectorMovement movement)
    {
        while (_itemSocket.IsOccupied == false)
        {
            if (movement.IsPathComplete())
            {
                _itemSocket.Collect(plant);
                Collected?.Invoke(plant);
                break;
            }

            yield return _delay;
        }
    }

    private IEnumerator BuildRoutine(Flag flag, CollectorMovement movement)
    {  
        while (flag.IsBuilt == false && flag != null)
        {
            if (movement.IsPathComplete())
            {
                flag.NotifyBuild();
                _isBuilding = false;
                _isBusy = false;
            }

            yield return _delay;
        }
    }

    public void StartDumping(DumpPlace dumpPlace, CollectorMovement movement)
    {
        _coroutineRunner.StartCoroutine(DumpRoutine(dumpPlace, movement));
    }

    private IEnumerator DumpRoutine(DumpPlace dumpPlace, CollectorMovement movement)
    {
        while (_itemSocket.IsOccupied)
        {
            if (movement.IsPathComplete())
            {
                _itemSocket.Dump();
                Dumped?.Invoke();
                _isBusy = false;
                break;
            }

            yield return _delay;
        }
    }
}
