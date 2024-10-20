using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Info : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    public void SetInfoText(string text)
    {
        infoText.text = text;
    }
}
