using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private ParticleSystem _scannedFX;    

    private Scanner _scanner;

    public bool IsScanned { get; private set; } = false;

    private void Awake()
    {
        _scannedFX.Stop();
       // _scanner.AreaScanned += InitScanned;

    }

    private void OnDestroy()
    {
       // _scanner.AreaScanned -= InitScanned;
    }

    public void Initialize(Scanner scanner)
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
