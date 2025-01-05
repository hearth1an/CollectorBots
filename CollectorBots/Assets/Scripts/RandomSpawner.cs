using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private Plant _prefab;
    [SerializeField] private float _radius;
    [SerializeField] private int _maxSpawned;

    public List<Plant> CreatedObjects { get; private set; } = new List<Plant>();
   
    private void OnEnable()
    {
        StartCoroutine(SpawnRoutine());
    }

    private Vector3 GetRandomPosition()
    {
        for (int i = 0; i < 10; i++) // 10 попыток найти точку
        {
            // Генерация случайной точки в радиусе
            Vector3 randomPoint = transform.position + new Vector3(
                Random.Range(-_radius, _radius),
                10f, // Поднимаем точку повыше, чтобы луч бил вниз
                Random.Range(-_radius, _radius)
            );

            // Raycast вниз от randomPoint
            if (Physics.Raycast(randomPoint, Vector3.down, out RaycastHit hit, 20f))
            {
                return hit.point; // Возвращаем точку на поверхности
            }
        }

        // Если не нашли подходящую точку, возвращаем текущую позицию спавнера
        return transform.position;
    }

    private IEnumerator SpawnRoutine()
    {
        int spawned = 0;
        WaitForSeconds delay = new WaitForSeconds(3f);

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
