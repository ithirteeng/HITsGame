using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEScipt : MonoBehaviour
{
    public GameObject canvas;

    private void Start()
    {
        canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        canvas.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canvas.SetActive(false);
    }
}
