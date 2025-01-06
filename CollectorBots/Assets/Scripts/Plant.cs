using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private ParticleSystem _scannedFX;
    [SerializeField] private Base _base;

    private Scanner _scanner;

    public bool IsScanned { get; private set; } = false;

    private void Awake()
    {
        _scannedFX.Stop();
        _scanner = _base.Scanner;
        _scanner.AreaScanned += InitScanned;

    }

    private void OnDisable()
    {
        _scanner.AreaScanned -= InitScanned;
    }

    private void InitScanned()
    {
        IsScanned = true;
        
        if (_scannedFX.isPlaying == false)
            _scannedFX.Play();
    }
}
