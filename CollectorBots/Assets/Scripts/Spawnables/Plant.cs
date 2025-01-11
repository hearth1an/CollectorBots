using System;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private ParticleSystem _scannedFX;

    public event Action<Plant> Destroyed;

    public bool IsScanned { get; private set; } = false;

    private void Start()
    {
        _scannedFX.Stop();
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke(this);
    }

    public void MarkAsScanned()
    {
        IsScanned = true;

        if (_scannedFX.isPlaying == false)
            _scannedFX.Play();
    }
}
