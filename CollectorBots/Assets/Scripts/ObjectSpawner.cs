using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected float _radius;
    [SerializeField] protected float _spawnDelay;
    [SerializeField] protected int _maxSpawned;

    public event Action<T> ObjectSpawned;

    public List<T> CreatedObjects { get; private set; } = new List<T>();
   
    public virtual void Awake()
    {
        StartCoroutine(SpawnRoutine());
    }

    public virtual Vector3 GetRandomPosition()
    {
        int tryHits = 10;
        float upwardsModifier = 15f;
        float maxDistance = 20f;

        for (int i = 0; i < tryHits; i++) 
        {            
            Vector3 randomPoint = transform.position + new Vector3(
                UnityEngine.Random.Range(-_radius, _radius),
                upwardsModifier,
                UnityEngine.Random.Range(-_radius, _radius)
            );
            
            if (Physics.Raycast(randomPoint, Vector3.down, out RaycastHit hit, maxDistance))
            {
                return hit.point;
            }
        }
        
        return transform.position;
    }

    public virtual IEnumerator SpawnRoutine()
    {
        int spawned = 0;

        WaitForSeconds delay = new WaitForSeconds(_spawnDelay);

        while (spawned < _maxSpawned)
        {
            var obj = Instantiate(_prefab, GetRandomPosition(), Quaternion.identity);
            ObjectSpawned?.Invoke(obj);
            CreatedObjects.Add(obj);

            spawned++;

            yield return delay;
        }
    }

    public virtual void SpawnNew(int count)
    {
        _maxSpawned = count;
        StartCoroutine(SpawnRoutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _radius);
    }
}
