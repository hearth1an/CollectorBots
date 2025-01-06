using UnityEngine;

public class PlantSpawner : ObjectSpawner<Plant>
{    
    [SerializeField] private int _minSpawned;
    [SerializeField] private int _newSpawnAmount;
    [SerializeField] private Base _base;

    public override Plant GetObject()
    {
        Plant plant = Instantiate(Prefab, GetRandomPosition(), Quaternion.identity);
        plant.Initialize(_base.Scanner);

        return plant;
    }

    public Plant GetRandomPlant()
    {
        Plant plant = null;
        int minValue = 0;

        for (int i = 0; i < CreatedObjects.Count; i++)
        {
            int index = Random.Range(minValue, CreatedObjects.Count - 1);

            if (CreatedObjects[index].IsScanned)
            {
                plant = CreatedObjects[index];
            }
        }

        if (CreatedObjects.Count < _minSpawned)
            SpawnNew(_newSpawnAmount);

        return plant;
    }
}
