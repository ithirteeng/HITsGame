using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortalAppearance : MonoBehaviour
{
    public GameObject portal;
    public GameObject pressEMessage;
    public TextMeshProUGUI message;
    public GameObject firstPot;
    public GameObject secondPot;
    private bool _isTrigger;
    private Collider2D trigger;
    private string _message = "Ёпта....";
    public PortalTrigger IsPortalTriggerred;


    private void Start()
    {
        trigger = GetComponent<Collider2D>();
        if (!IsPortalTriggerred.isPortalTriggered)
        {
            portal.SetActive(false);
            firstPot.SetActive(true);
            secondPot.SetActive(false);
        }
        else
        {
            firstPot.SetActive(false);
            secondPot.SetActive(true);
            trigger.enabled = false;
            portal.SetActive(true);
        }
    }

    private void Update()
    {
        if (_isTrigger)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                message.text = _message;
                pressEMessage.SetActive(false);
                firstPot.SetActive(false);
                secondPot.SetActive(true);
                PortalAppearanceFunction();
                IsPortalTriggerred.isPortalTriggered = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        _isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (message.text == _message)
        {
            trigger.enabled = false;
        }

        _isTrigger = false;
    }

    private void PortalAppearanceFunction()
    {
        portal.SetActive(true);
    }

    private void OnApplicationQuit()
    {
        IsPortalTriggerred.isPortalTriggered = false;
    }
}