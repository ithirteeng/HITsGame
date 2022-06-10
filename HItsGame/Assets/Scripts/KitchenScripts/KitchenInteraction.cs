using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenInteraction : MonoBehaviour
{
    public GameObject messageBox;
    public GameObject pressEMessage;

    private void Start()
    {
        makeActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        makeActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        makeActive(false);
    }

    private void makeActive(bool flag)
    {
        messageBox.SetActive(flag);
        pressEMessage.SetActive(flag);
    }
}