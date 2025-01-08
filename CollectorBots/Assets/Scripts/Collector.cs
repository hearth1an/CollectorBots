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

    private void Start()
    {
        Debug.Log(DumpPlace);
        DumpPlace = Base.DumpPlace;
        //Debug.Log(_dumpPlace.transform.position);
        //_itemSocket.PlantDumped += DumpPlace.UpdateCounter; // тут
    }

    private void OnDestroy()
    {        
        //_itemSocket.PlantDumped -= DumpPlace.UpdateCounter;
    }

    public bool IsDoingTask()
    {
        return TaskHandler.IsBusy;
    }

    public void Initialize(Base baseObject)
    {
        Base = baseObject;
        
    }

    public void SetTarget(Plant plant)
    {    
        Mover.GoTo(plant.transform.position);
        TaskHandler.TaskCollect(plant);
    }  
}
