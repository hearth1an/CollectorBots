using UnityEngine;
using System.Collections;

public class CollectorsSpawner : ObjectSpawner<Collector>
{
    [SerializeField] private Base _base;

    private void Start()
    {
        _base.DumpPlace.CollectorPriceCollected += SpawnSingle;
    }

    private void OnDestroy()
    {
        _base.DumpPlace.CollectorPriceCollected -= SpawnSingle;
    }

    public void AssignCollector(Collector collector)
    {
        SetStartSpawnedCount();
        AddObject(collector);
    }

    public override Collector GetObject()
    {
        Collector collector = Instantiate(Prefab, GetRandomPosition(), Quaternion.identity);
        collector.Initialize(_base);

        return collector;
    }

    public bool CanSpawnSingle()
    {
        if (_base.IsFlagPlaced() == false)
        {            
            _base.DumpPlace.AllowPayment();
            return true;
        }

        _base.DumpPlace.CancelPayment();

        return false;
    }

    private void SpawnSingle()
    {
        int count = 1;

        if (CanSpawnSingle())
        {
            Spawn(count);
        }
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