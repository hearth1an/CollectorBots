using System;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public bool IsBuilt { get; private set; } = false;

    public event Action<Flag> BaseBuilt;
    public event Action FlagDestroyed;

    public void NotifyBuild()
    {
        BaseBuilt?.Invoke(this);

        IsBuilt = true;

        FlagDestroyed?.Invoke();
    }
}
