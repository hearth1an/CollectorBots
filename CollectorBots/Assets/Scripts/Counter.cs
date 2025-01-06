using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _value = 0;

    private void Awake()
    {
        _text.text = _value.ToString();
    }

    public void UpdateValue()
    {
         _value++;

        _text.text = _value.ToString();
    }
}
