using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenInteraction : MonoBehaviour
{
    public GameObject messageBox;
    public GameObject pressEMessage;
    public PortalTrigger isPortalTrigger;

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
        if (!isPortalTrigger.isPortalTriggered)
        {
            pressEMessage.SetActive(flag);
        }
    }
}