using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ObjectSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected float Radius;
    [SerializeField] protected float SpawnDelay;
    [SerializeField] protected int MaxSpawned;

    private readonly List<T> _createdObjects = new List<T>();

    public event Action<T> ObjectSpawned;    

    public IReadOnlyList<T> CreatedObjects => _createdObjects;

    public virtual void Awake()
    {
        StartCoroutine(SpawnRoutine());
    }

    protected void AddObject(T obj)
    {
        _createdObjects.Add(obj);
        ObjectSpawned?.Invoke(obj);
    }

    public void SetStartSpawnedCount()
    {
        int value = 0;
        MaxSpawned = value;
    }

    public virtual Vector3 GetRandomPosition()
    {
        int tryHits = 10;
        float upwardsModifier = 15f;
        float maxDistance = 20f;

        for (int i = 0; i < tryHits; i++) 
        {            
            Vector3 randomPoint = transform.position + new Vector3(
                UnityEngine.Random.Range(-Radius, Radius),
                upwardsModifier,
                UnityEngine.Random.Range(-Radius, Radius)
            );
            
            if (Physics.Raycast(randomPoint, Vector3.down, out RaycastHit hit, maxDistance))
            {
                return hit.point;
            }
        }
        
        return transform.position;
    }

    public abstract T GetObject();

    public abstract IEnumerator SpawnRoutine();
    

    public virtual void Spawn(int count)
    {
        MaxSpawned = count;
        StartCoroutine(SpawnRoutine());
    }

    protected void RemoveObject(T obj)
    {
        if (CreatedObjects.Contains(obj))
        {
            _createdObjects.Remove(obj);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }
}
