using UnityEngine;
using System.Collections;

public interface ICoroutineRunner
{
    Coroutine StartCoroutine(IEnumerator routine);
}