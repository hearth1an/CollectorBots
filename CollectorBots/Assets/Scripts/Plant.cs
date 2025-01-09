using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private ParticleSystem _scannedFX;

    public bool IsScanned { get; private set; } = false;

    private void Start()
    {
        _scannedFX.Stop();
    }

    public void MarkAsScanned()
    {
        IsScanned = true;

        if (_scannedFX.isPlaying == false)
            _scannedFX.Play();
    }
}
