using System;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TMP_Text clockText;

    private void Update()
    {
        clockText.text = DateTime.Now.ToShortTimeString();
    }
}
