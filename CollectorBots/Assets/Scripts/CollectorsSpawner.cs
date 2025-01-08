using UnityEngine;

public class CollectorsSpawner : ObjectSpawner<Collector>
{
    [SerializeField] private Base _base;

    public override Collector GetObject()
    {
        Collector collector = Instantiate(Prefab, GetRandomPosition(), Quaternion.identity);
        collector.Initialize(_base);

        return collector;
    }

    public bool HasAvailableCollectors()
    {
        foreach (Collector collector in CreatedObjects)
        {
            if (collector.IsDoingTask() == false)
            {
                return true;
            }
        }

        return false;
    }
}
