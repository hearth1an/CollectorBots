using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : RandomSpawner<Plant>
{
    public int ScannedCount { get; private set; } = 0;

    public Plant GetRandomPlant()
    {
        Plant plant = null;

        for (int i = 0; i < CreatedObjects.Count; i++)
        {
            int index = Random.Range(0, CreatedObjects.Count - 1);

            if (CreatedObjects[index].IsScanned)
            {
                plant = CreatedObjects[index];
            }
        }

        if (CreatedObjects.Count < 3)
        {
            UpdateSpawnCount(10);
        }

        return plant;
    }

    public void RemoveObject(Plant plant)
    {
        CreatedObjects.Remove(plant);
    }
}
