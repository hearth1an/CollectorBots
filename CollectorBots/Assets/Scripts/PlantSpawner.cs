using UnityEngine;

public class PlantSpawner : ObjectSpawner<Plant>
{    
    [SerializeField] private int _minSpawned;
    [SerializeField] private int _newSpawnAmount;

    public override Plant GetObject()
    {
        TrySpawnNew();

        return Instantiate(Prefab, GetRandomPosition(), Quaternion.identity);
    }

    private void TrySpawnNew()
    {
        if (CreatedObjects.Count < _minSpawned)
            SpawnNew(_newSpawnAmount);
    }
}
