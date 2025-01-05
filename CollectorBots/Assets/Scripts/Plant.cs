using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private ParticleSystem _scannedFX;
    private Scanner _scanner;

    private void Awake()
    {
        _scannedFX.Stop();
        _scanner = FindObjectOfType<Scanner>();
        _scanner.AreaScanned += InitScanned;
    }

    private void OnDisable()
    {
        _scanner.AreaScanned -= InitScanned;
    }

    private void InitScanned()
    {
        if (_scannedFX.isPlaying == false)
            _scannedFX.Play();
    }

}
