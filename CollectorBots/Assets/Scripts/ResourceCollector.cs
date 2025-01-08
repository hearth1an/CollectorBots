using System;
using System.Collections;
using UnityEngine;

public class ResourceCollector
{
    private ItemSocket _itemSocket;
    private MonoBehaviour _owner;

    private float _taskDelay = 1f;
    private WaitForSeconds _delay;
    private bool _isBusy;

    public bool IsBusy => _isBusy;

    public event Action<Plant> Collected;
    public event Action Dumped;

    public ResourceCollector(ItemSocket itemSocket, MonoBehaviour owner)
    {
        _itemSocket = itemSocket;
        _owner = owner;
        _delay = new WaitForSeconds(_taskDelay);
    }

    public void SetTarget(Plant plant, CollectorMovement movement)
    {
        _isBusy = true;
        movement.GoTo(plant.transform.position);
        _owner.StartCoroutine(CollectRoutine(plant, movement));
    }

    private IEnumerator CollectRoutine(Plant plant, CollectorMovement movement)
    {
        while (_itemSocket.IsOccupied == false)
        {
            if (movement.IsPathEnding())
            {
                _itemSocket.Collect(plant);
                Collected?.Invoke(plant);
                break;
            }

            yield return _delay;
        }
    }

    public void StartDumping(DumpPlace dumpPlace, CollectorMovement movement)
    {
        _owner.StartCoroutine(DumpRoutine(dumpPlace, movement));
    }

    private IEnumerator DumpRoutine(DumpPlace dumpPlace, CollectorMovement movement)
    {
        while (_itemSocket.IsOccupied)
        {
            if (movement.IsPathEnding())
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
