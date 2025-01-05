using System;
using UnityEngine;
using UnityEngine.UI;

public class Scanner : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Button _button;

    public event Action AreaScanned;

    private void Awake()
    {
        _button.onClick.AddListener(PlayAnimation);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(PlayAnimation);
    }

    private void PlayAnimation()
    {
        if (_particleSystem.isPlaying == false)
        {
            _particleSystem.Play();
            AreaScanned?.Invoke();
        }
    }
}
