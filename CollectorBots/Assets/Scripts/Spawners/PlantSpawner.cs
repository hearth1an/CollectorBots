using UnityEngine;
using System.Collections;

public class PlantSpawner : ObjectSpawner<Plant>
{    
    [SerializeField] private int _minSpawned;
    [SerializeField] private int _newSpawnAmount;

    public override Plant GetObject()
    {
        TrySpawnNew();

        var plant = Instantiate(Prefab, GetRandomPosition(), Quaternion.identity);

        plant.Destroyed += RemovePlant;

        return plant;
    }

    public override IEnumerator SpawnRoutine()
    {
        int spawned = 0;

        WaitForSeconds delay = new WaitForSeconds(SpawnDelay);

        while (enabled)
        {
            yield return delay;

            if (CreatedObjects.Count < MaxSpawned)
            {
                var obj = GetObject();
                AddObject(obj);

                spawned++;
            }
        }
    }

    private void TrySpawnNew()
    {
        if (CreatedObjects.Count < _minSpawned)
            Spawn(_newSpawnAmount);
    }

    private void RemovePlant(Plant plant)
    {
        plant.Destroyed -= RemovePlant;
        RemoveObject(plant);        
    }    
}
