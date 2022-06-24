using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToiletMessage : MonoBehaviour
{
    public GameObject portal;
    public GameObject messageCanvas;
    public TextMeshProUGUI text;

    private void Start()
    {
        text.text = "ЧЁЁЁЁЁЁЁЁЁРТ";
        messageCanvas.SetActive(true);
        portal.SetActive(true);
    }

}
