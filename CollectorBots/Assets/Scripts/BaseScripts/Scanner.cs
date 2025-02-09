using System;
using System.Collections;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius = 10f;
    [SerializeField] private LayerMask _resourceLayer;
    [SerializeField] private float _scanDelay = 5f;
    [SerializeField] private ScannerAnimator _scannerAnimator;

    public event Action<Plant[]> ResourcesDetected;

    private void Start()
    {
        StartCoroutine(ScanRoutine());
    }

    private IEnumerator ScanRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(_scanDelay);

        while (enabled)
        {            
            _scannerAnimator.PlayAnimation();

            Plant[] plants = ScanArea();
            
            if (plants.Length > 0)
            {
                ResourcesDetected?.Invoke(plants);
            }

            yield return delay;
        }
    }

    private Plant[] ScanArea()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius, _resourceLayer);

        return Array.ConvertAll(hits, hit => hit.GetComponent<Plant>());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }
}
