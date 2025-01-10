using UnityEngine;

public class ScannerAnimatorController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void PlayAnimation()
    {
        if (!_particleSystem.isPlaying)
        {
            _particleSystem.Play();
        }
    }
}
