using UnityEngine;

public class PlantSpawner : ObjectSpawner<Plant>
{    
    [SerializeField] private int _minSpawned;
    [SerializeField] private int _newSpawnAmount;
    [SerializeField] private Base _base;

    public int ScannedCount => GetScannedCount();

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
            int index = Random.Range(minValue, CreatedObjects.Count);

            if (CreatedObjects[index].IsScanned)
            {
                plant = CreatedObjects[index];
                CreatedObjects.Remove(plant);
            }
        }

        if (CreatedObjects.Count < _minSpawned)
            SpawnNew(_newSpawnAmount);

        return plant;
    }

    private int GetScannedCount()
    {
        int count = 0;

        foreach (Plant plant in CreatedObjects)
        {
            if (plant.IsScanned)
            {
                count++;
            }
        }

        return count;
    }
}
