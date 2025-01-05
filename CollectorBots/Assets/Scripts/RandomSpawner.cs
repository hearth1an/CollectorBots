using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RandomSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected float _radius;
    [SerializeField] protected float _spawnDelay;
    [SerializeField] protected int _maxSpawned;

    public List<T> CreatedObjects { get; private set; } = new List<T>();
   
    public virtual void OnEnable()
    {
        StartCoroutine(SpawnRoutine());
    }

    public virtual Vector3 GetRandomPosition()
    {
        for (int i = 0; i < 10; i++) 
        {            
            Vector3 randomPoint = transform.position + new Vector3(
                Random.Range(-_radius, _radius),
                10f,
                Random.Range(-_radius, _radius)
            );
            
            if (Physics.Raycast(randomPoint, Vector3.down, out RaycastHit hit, 20f))
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
            CreatedObjects.Add(obj);

            spawned++;
            yield return delay;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, _radius);
    }
}
