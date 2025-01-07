using UnityEngine;
using TMPro;

public class View : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Counter _counter;

    private void Awake()
    {
        UpdateText(_counter.Value);
        _counter.CountUpdated += UpdateText;
    }

    private void OnDestroy()
    {
        _counter.CountUpdated -= UpdateText;
    }

    private void UpdateText(int value)
    {
        _text.text = _counter.Value.ToString();
    }

}
