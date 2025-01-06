using System;
using System.Collections;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private float _scanDelay = 10f;

    public event Action AreaScanned;

    private void Awake()
    {
        StartCoroutine(ScanRoutine());
    }

    private IEnumerator ScanRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(_scanDelay);

        

        while (enabled)
        {
            PlayAnimation();            

            yield return delay;
        }
    }

    private void PlayAnimation()
    {
        AreaScanned?.Invoke();

        if (_particleSystem.isPlaying == false)
        {
            _particleSystem.Play();            
        }
    }
}
