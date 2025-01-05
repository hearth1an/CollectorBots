using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int value = 0;

    private void Awake()
    {
        _text.text = value.ToString();
    }

    public void UpdateValue()
    {
         value++;

        _text.text = value.ToString();
    }
}
