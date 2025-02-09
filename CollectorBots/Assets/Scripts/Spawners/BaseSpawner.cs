using System.Collections;
using UnityEngine;

public class BaseSpawner : ObjectSpawner<Base>
{
    private Flag _flag;

    public override Base GetObject()
    {       
        if (_flag != null)
        {
            var newBase = Instantiate(Prefab, _flag.gameObject.transform.position, Prefab.transform.rotation);

            return newBase;
        }

        return null;
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

    public void Init(Flag flag, Collector collector)
    {
        _flag = flag;

        Base newBase = GetObject();
        collector.Init(newBase);

        newBase.Initialize(this);        
    }
}
