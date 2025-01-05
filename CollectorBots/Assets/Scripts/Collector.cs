using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Collector : MonoBehaviour
{
    [SerializeField] private ItemSocket _itemSocket;
    [SerializeField] private Transform _containers;

    private Vector3 _chillZone;

    private NavMeshAgent _agent;

    private Vector3 _currentTarget;

    private bool _isBusy = false;

    private void Awake()
    {
        _chillZone = transform.position;

        _agent = GetComponent<NavMeshAgent>();
        _itemSocket.PlantCollected += ReturnToBase;
    }

    public void SetCollectTarget(Plant target)
    {
        //_currentTarget = target;
        _isBusy = true;
        _agent.SetDestination(target.transform.position);

        StartCoroutine(TryCollect(target));
    }

    private bool IsPathEnding()
    {
        return _agent.hasPath && _agent.remainingDistance < 1f;
    }
    
    private IEnumerator TryCollect(Plant plant)
    {
        WaitForSeconds delay = new WaitForSeconds(1);
        
        while (_itemSocket.IsOccupied != true)
        {            
            if (IsPathEnding())
            {
                _itemSocket.Collect(plant);
                StopCoroutine(TryCollect(plant));
            }

            yield return delay;
        }
    }

    private void ReturnToBase()
    {
        Debug.Log("На базу");
        _agent.ResetPath();
        _agent.SetDestination(_containers.position);

        StartCoroutine(TryUnload());
    }

    private void Chill()
    {
        _agent.ResetPath();
        _agent.SetDestination(_chillZone);
    }

    private IEnumerator TryUnload()
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        while (_itemSocket.IsOccupied)
        {
            if (IsPathEnding())
            {
                _itemSocket.Dump();
                Chill();
                StopCoroutine(TryUnload());
                Debug.Log("С");
            }

            yield return delay;
        }
    }

    private void Update()
    {
      // Debug.Log(_agent.destination);
       Debug.Log(_agent.pathStatus);
       //Debug.Log(_agent.hasPathdddd);
    }
}
