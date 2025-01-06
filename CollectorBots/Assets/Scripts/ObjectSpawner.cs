using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected float Radius;
    [SerializeField] protected float SpawnDelay;
    [SerializeField] protected int MaxSpawned;

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

    public virtual IEnumerator SpawnRoutine()
    {
        int spawned = 0;

        WaitForSeconds delay = new WaitForSeconds(SpawnDelay);

        while (spawned < MaxSpawned)
        {
            var obj = Instantiate(Prefab, GetRandomPosition(), Quaternion.identity);
            ObjectSpawned?.Invoke(obj);
            CreatedObjects.Add(obj);

            spawned++;

            yield return delay;
        }
    }

    public virtual void SpawnNew(int count)
    {
        MaxSpawned = count;
        StartCoroutine(SpawnRoutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }
}
