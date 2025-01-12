using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    private Color _color;

    private void Awake()
    {
        _color = _renderer.material.color;
    }

    private void OnMouseEnter()
    {
        _renderer.material.color = Color.blue;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _color;
    }

    private void ChangeColor() => _renderer.material.color = Color.blue;

    private void OnMouseDown()
    {
        
    }
}
