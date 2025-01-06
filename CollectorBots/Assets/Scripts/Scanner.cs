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
            AreaScanned?.Invoke();
            PlayAnimation();            

            yield return delay;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        // Проверяем, есть ли у объекта компонент Plant
        if (other.TryGetComponent<Plant>(out Plant plant))
        {
            Debug.Log(plant);
            plant.InitScanned(); // Вызываем метод у Plant
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
