using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;

    public bool IsBuilt { get; private set; } = false;

    public void BuildBase()
    {
        Debug.Log("Building");
        Instantiate(_basePrefab, gameObject.transform.position, Quaternion.identity);

        //builtBase.transform.rotation = _basePrefab.transform.rotation;

        IsBuilt = true;
    }
}
