using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private ParticleSystem _scannedFX;    

    private Scanner _scanner;

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

    public void Initialize(Scanner scanner)
    {
        _scanner = scanner;
    }

    private void InitScanned()
    {
        IsScanned = true;
        
        if (_scannedFX.isPlaying == false)
            _scannedFX.Play();
    }
}
