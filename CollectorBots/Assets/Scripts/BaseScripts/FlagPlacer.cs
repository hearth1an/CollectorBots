using UnityEngine;

public class FlagPlacer : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;

    public Flag Spawn(Vector3 position)
    {
        var flag = Instantiate(_flagPrefab, position, Quaternion.identity);

        return flag;
    }
}
