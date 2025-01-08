using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CollectorMover : Collector
{
    private NavMeshAgent _agent;
    private Vector3 _chillZone;

    private void Awake()
    {
        Socket.PlantTaken += ReturnToBase;
        _agent = GetComponent<NavMeshAgent>();
        _chillZone = transform.position;
    }

    private void OnDestroy()
    {
        Socket.PlantTaken -= ReturnToBase;
    }

    public void GoTo(Vector3 target)
    {        
        _agent.ResetPath();
        _agent.SetDestination(target);
    }

    public bool IsPathEnding()
    {
        float minDistance = 3;
        return _agent.hasPath && _agent.remainingDistance < minDistance;
    }
    
    protected void ReturnToBase()
    {
        Debug.Log(DumpPlace);
        GoTo(DumpPlace.transform.position);
    }

    public void GoChill()
    {
        GoTo(_chillZone);
    }
}
