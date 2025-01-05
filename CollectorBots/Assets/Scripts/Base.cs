using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private CollectorsSpawner _unitSpawner;
    [SerializeField] private PlantSpawner _plantsSpawner;
    [SerializeField] private Button _button;

    private void Awake()
    {        
        _button.onClick.AddListener(GiveTask);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(GiveTask);
    }

    private void GiveTask()
    {
        foreach (Collector collector in _unitSpawner.CreatedObjects)
        {
            Plant plant = _plantsSpawner.GetRandomPlant();            

            if (collector.IsBusy == false && plant != null)
            {
                collector.SetTarget(plant);
                _plantsSpawner.CreatedObjects.Remove(plant);
            }            
        }       
    }
}
