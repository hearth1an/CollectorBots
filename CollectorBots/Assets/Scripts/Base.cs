using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private ScannerAnimController _animController;
    [SerializeField] private CollectorsSpawner _collectorsSpawner;
    [SerializeField] private DumpPlace _dumpPlace;

    public DumpPlace DumpPlace => _dumpPlace;
    public Scanner Scanner => _scanner;

    private void Start()
    {
        _scanner.ResourcesDetected += AssignResourcesToBots;
    }

    private void OnDestroy()
    {
        _scanner.ResourcesDetected -= AssignResourcesToBots;
    }

    private void AssignResourcesToBots(Plant[] plants)
    {
        if (plants.Length == 0) return;

        _animController.PlayAnimation();

        foreach (Collector collector in _collectorsSpawner.CreatedObjects)
        {
            if (!collector.IsBusy)
            {
                foreach (Plant plant in plants)
                {
                    if (plant.IsScanned == false)
                    {
                        plant.MarkAsScanned();
                        collector.SetTarget(plant);
                        break;
                    }
                }
            }
        }
    }
}
