using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Collector : MonoBehaviour
{    
    [SerializeField] private ItemSocket _itemSocket;

    private Base _base;
    private DumpPlace _dumpPlace;
    private float _taskDelay = 1f;
    private WaitForSeconds _delay;
    private Vector3 _chillZone;
    private NavMeshAgent _agent;

    public event Action<Plant> Collected;    

    public bool IsBusy { get; private set; } = false;

    private void Start()
    {
        _delay = new WaitForSeconds(_taskDelay);
        _chillZone = transform.position;

        _dumpPlace = _base.DumpPlace;
        _agent = GetComponent<NavMeshAgent>();

        _itemSocket.PlantTaken += ReturnToBase;
        _itemSocket.PlantDumped += _dumpPlace.UpdateCounter;
    }

    private void OnDestroy()
    {
        _itemSocket.PlantTaken -= ReturnToBase;
        _itemSocket.PlantDumped -= _dumpPlace.UpdateCounter;
    }

    public void Initialize(Base baseObject) => _base = baseObject;

    public void SetTarget(Plant plant)
    {       
        IsBusy = true;
        GoTo(plant.transform.position);
        StartCoroutine(CollectRoutine(plant));
    }

    private bool IsPathEnding()
    {
        float minDistance = 3;

        return _agent.hasPath && _agent.remainingDistance < minDistance;
    }

    private void ReturnToBase()
    {
        GoTo(_dumpPlace.transform.position);
        StartCoroutine(DumpRoutine());
    }

    private void GoChill()
    {
        
        StopAllCoroutines();
        GoTo(_chillZone);       
    }

    private void GoTo(Vector3 target)
    {
        _agent.ResetPath();
        _agent.SetDestination(target);
    }

    private IEnumerator CollectRoutine(Plant plant)
    {
        while (enabled && _itemSocket.IsOccupied == false)
        {
            if (IsPathEnding())
            {
                _itemSocket.Collect(plant);
                Collected?.Invoke(plant);
            }

            yield return _delay;
        }
    }

    private IEnumerator DumpRoutine()
    {    
        while (enabled && _itemSocket.IsOccupied)
        {
            if (IsPathEnding())
            {               
                _itemSocket.Dump();
                IsBusy = false;
                GoChill();               
            }

            yield return _delay;
        }
    }
}
