using UnityEngine;

public class CollectorsSpawner : ObjectSpawner<Collector>
{
    [SerializeField] private Base _base;

    public int FreeCollectors => GetFreeCollectorsCount();

    public override Collector GetObject()
    {
        Collector collector = Instantiate(Prefab, GetRandomPosition(), Quaternion.identity);
        collector.Initialize(_base);

        return collector;
    }

    private int GetFreeCollectorsCount()
    {
        int count = 0;

        foreach (Collector collector in CreatedObjects)
        {
            if (collector.IsBusy == false)
            {
                count++;
            }
        }

        return count;
    }
}
