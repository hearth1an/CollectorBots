using System;
using System.Collections;
using UnityEngine;

public class ScannerAnimController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private float _scanDelay = 5f;

    public event Action AreaScanned;

    private void OnEnable()
    {
        StartCoroutine(ScanRoutine());
    }

    private IEnumerator ScanRoutine()
    {        
        WaitForSeconds delay = new WaitForSeconds(_scanDelay);

        while (enabled)
        {
            yield return delay;

            PlayAnimation();

            AreaScanned?.Invoke();
        }
    }
    
    private void PlayAnimation()
    {
        if (_particleSystem.isPlaying == false)
        {
            _particleSystem.Play();            
        }
    }
}
