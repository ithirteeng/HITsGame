using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class AnotherDoorTriggers : MonoBehaviour
{
    public GameObject messageBox;

    private void Start()
    {
        messageBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        messageBox.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        messageBox.SetActive(false);
    }
}
