using UnityEngine;
using System.Collections;

public class CollectorsSpawner : ObjectSpawner<Collector>
{
    [SerializeField] private Base _base;

    public override Collector GetObject()
    {
        Collector collector = Instantiate(Prefab, GetRandomPosition(), Quaternion.identity);
        collector.Initialize(_base);

        return collector;
    }

    public override IEnumerator SpawnRoutine()
    {
        int spawned = 0;

        WaitForSeconds delay = new WaitForSeconds(SpawnDelay);

        while (spawned < MaxSpawned)
        {
            yield return delay;

            var obj = GetObject();
            AddObject(obj);

            spawned++;
        }
    }

    public bool HasAvailableCollectors()
    {
        foreach (Collector collector in CreatedObjects)
        {
            if (collector.IsBusy == false)
            {
                return true;
            }
        }

        return false;
    }
}
