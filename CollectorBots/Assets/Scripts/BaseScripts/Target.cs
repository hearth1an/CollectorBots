using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private LayerMask _groundLayer;

    private Color _color;
    private Flag _currentFlag;
    private bool _isFlagPlacementMode = false; // Режим установки флага
    private bool _isBaseSelected = false; // Активна ли база

    private void Awake()
    {
        _color = _renderer.material.color;
    }

    private void OnMouseEnter()
    {
        if (!_isBaseSelected)
        {
            _renderer.material.color = Color.blue;
        }
    }

    private void OnMouseExit()
    {
        if (!_isBaseSelected)
        {
            _renderer.material.color = _color;
        }
    }

    private void OnMouseDown()
    {
        if (!_isFlagPlacementMode)
        {
            _isFlagPlacementMode = true;
            _isBaseSelected = true;
            _renderer.material.color = Color.blue;
        }
    }

    private void Update()
    {
        if (_isFlagPlacementMode && Input.GetMouseButtonDown(0))
        {
            if (IsClickOnBase()) return; // Игнорируем клики на базу
            if (TryPlaceFlag())
            {
                _isFlagPlacementMode = false;
                _isBaseSelected = false;
                _renderer.material.color = _color;
            }
        }
    }

    private bool TryPlaceFlag()
    {
        Vector3? position = GetClickPositionOnGround();
        if (position.HasValue)
        {
            if (_currentFlag == null)
            {
                _currentFlag = Instantiate(_flagPrefab, position.Value, Quaternion.identity);
            }
            else
            {
                _currentFlag.transform.position = position.Value;
            }
            return true;
        }
        return false;
    }

    private Vector3? GetClickPositionOnGround()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            return hit.point; // Убираем проверку на близость
        }
        return null;
    }

    private bool IsClickOnBase()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform == this.transform; // Проверяем, был ли клик по этой базе
        }
        return false;
    }
}
