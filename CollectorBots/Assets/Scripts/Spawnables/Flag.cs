using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;

    public bool IsBuilt { get; private set; } = false;

    public event Action<Base> BaseBuilt;

    public void BuildBase()
    {
        var newBase = Instantiate(_basePrefab, gameObject.transform.position, _basePrefab.transform.rotation);

        BaseBuilt?.Invoke(newBase);

        IsBuilt = true;

        Destroy(gameObject);
    }
}
