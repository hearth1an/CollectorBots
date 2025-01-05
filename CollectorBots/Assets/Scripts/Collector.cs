using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Collector : MonoBehaviour
{
    [SerializeField] private ItemSocket _itemSocket;

    public event Action <Plant>DumpedOk;

    private Route _containers;

    private Vector3 _chillZone;

    private NavMeshAgent _agent;

    private Vector3 _currentTarget;

    private bool _isBusy = false;

    private void Awake()
    {
        _containers = FindObjectOfType<Route>();

        Debug.Log(_containers.transform.position);
        _chillZone = transform.position;

        _agent = GetComponent<NavMeshAgent>();
        _itemSocket.PlantCollected += ReturnToBase;

    }

    public void SetCollectTarget(Plant plant)
    {
        _agent.ResetPath();
        _isBusy = true;

        _agent.SetDestination(plant.transform.position);

        StartCoroutine(TryCollect(plant));
    }

    private bool IsPathEnding()
    {
        return _agent.hasPath && _agent.remainingDistance < 2.5f;
    }
    
    private IEnumerator TryCollect(Plant plant)
    {
        WaitForSeconds delay = new WaitForSeconds(1);
        

        while (enabled && _itemSocket.IsOccupied == false)
        {            
            if (IsPathEnding())
            {
                Debug.Log("trying collect");
                _itemSocket.Collect(plant);

                StopCoroutine(TryCollect(plant));
            }

            yield return delay;
        }
    }

    private void ReturnToBase()
    {
        Debug.Log("move to base");

        _agent.ResetPath();
        _agent.SetDestination(_containers.transform.position);      

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
       
        while (enabled && _itemSocket.IsOccupied)
        {
            Debug.Log("Dumping2");
            Debug.Log(IsPathEnding());

            if (IsPathEnding())
            {
                Debug.Log("Dumping3");
                _itemSocket.Dump();
                Chill();
            }

            yield return delay;
        }
    }

}
