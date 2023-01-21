using System;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    public TMP_Text clockText;

    private void Update()
    {
        clockText.text = DateTime.Now.ToShortTimeString();
    }
}
