using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    //[SerializeField] private ItemSocket _socket;

    private int count = 0;

    private void Awake()
    {
        _text.text = count.ToString();
       // _socket.PlantDumped += UpdateCount;
    }

    public void UpdateCount()
    {
        count++;

        _text.text = count.ToString();
    }

}
