using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class messageAppearance : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject canvas;
    public GameObject button;

    private void Start()
    {
        text.text = "Офигеть, счастливая украинская семья!!";
        button.SetActive(false);
        canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        canvas.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canvas.SetActive(false);
    }
}