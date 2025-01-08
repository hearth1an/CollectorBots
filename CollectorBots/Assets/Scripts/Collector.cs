using UnityEngine;


public class Collector : MonoBehaviour
{    
    [SerializeField] private ItemSocket _itemSocket;
    [SerializeField] private CollectorMover _collectorMover;
    [SerializeField] private CollectorTaskHandler _taskHandler;
    
    protected CollectorMover Mover => _collectorMover;
    protected ItemSocket Socket => _itemSocket;
    protected CollectorTaskHandler TaskHandler => _taskHandler;

    protected Base Base;
    protected DumpPlace DumpPlace;

    public bool IsDoingTask()
    {
        return TaskHandler.IsBusy;
    }

    public void Initialize(Base baseObject)
    {
        Base = baseObject;
        DumpPlace = baseObject.DumpPlace;
        Debug.Log(DumpPlace);

    }

    public void SetTarget(Plant plant)
    {    
        Mover.GoTo(plant.transform.position);
        TaskHandler.TaskCollect(plant);
    }  
}
