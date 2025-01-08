using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CollectorMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
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
}
