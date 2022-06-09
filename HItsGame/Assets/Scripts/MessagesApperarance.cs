using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagesApperarance : MonoBehaviour
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