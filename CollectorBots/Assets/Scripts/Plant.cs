using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private ParticleSystem _scannedFX;
    [SerializeField] private ScannerAnimController _scanner;

    public bool IsScanned { get; private set; } = false;

    private void Start()
    {
        _scannedFX.Stop();
        _scanner.AreaScanned += InitScanned;
    }

    private void OnDestroy()
    {
       _scanner.AreaScanned -= InitScanned;
    }

    public void Initialize(ScannerAnimController scanner)
    {
        _scanner = scanner;
    }

    public void InitScanned()
    {
        IsScanned = true;
        
        if (_scannedFX.isPlaying == false)
            _scannedFX.Play();
    }
}
