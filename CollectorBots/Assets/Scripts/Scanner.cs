using System;
using System.Collections;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private float _scanDelay = 10f;

    public event Action AreaScanned;

    private void Start()
    {
        StartCoroutine(ScanRoutine());
    }

    private IEnumerator ScanRoutine()
    {
        
        WaitForSeconds delay = new WaitForSeconds(_scanDelay);

        while (enabled)
        {
            //Debug.Log("Area scanned");
            AreaScanned?.Invoke();
            PlayAnimation();            

            yield return delay;
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
