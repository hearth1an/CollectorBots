using UnityEngine;
using System;

[RequireComponent(typeof(CollectorMovement))]
public class Collector : MonoBehaviour
{
    [SerializeField] private ItemSocket _itemSocket;

    private Base _base;
    private DumpPlace _dumpPlace;
    private CollectorMovement _movement;
    private ResourceCollector _resourceCollector;

    public ResourceCollector ResourceCollector => _resourceCollector;

    public bool IsBusy => _resourceCollector.IsBusy;

    public event Action<Plant> Collected;

    public void Initialize(Base baseObject) => _base = baseObject;

    private void Awake()
    {
        _movement = GetComponent<CollectorMovement>();
        _resourceCollector = new ResourceCollector(_itemSocket, this);
    }

    private void Start()
    {
        _dumpPlace = _base.DumpPlace;

        _itemSocket.PlantTaken += ReturnToBase;
        _itemSocket.PlantDumped += _dumpPlace.UpdateCounter;

        _resourceCollector.Collected += plant => Collected?.Invoke(plant);
    }

    private void OnDestroy()
    {
        _itemSocket.PlantTaken -= ReturnToBase;
        _itemSocket.PlantDumped -= _dumpPlace.UpdateCounter;
    }    

    public void SetTarget(Plant plant)
    {
        _resourceCollector.SetTarget(plant, _movement);
    }

    private void ReturnToBase()
    {
        _movement.GoTo(_dumpPlace.transform.position);
        _resourceCollector.StartDumping(_dumpPlace, _movement);
    }
}
