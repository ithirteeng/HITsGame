using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalAppearance : MonoBehaviour
{
    public GameObject portal;
    public GameObject pressEMessage;
    public TextMeshProUGUI message;
    public GameObject firstPot;
    public GameObject secondPot;
    public GameObject player;
    private bool _isTrigger;
    private Collider2D trigger;
    private const string MESSAGE = "Ёпта....";
    public PortalTrigger IsPortalTriggerred;


    private void Start()
    {
        PlayerAppearance.Init(player);
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
                SceneManager.LoadScene("MinigameScene", LoadSceneMode.Additive);
                player.SetActive(false);
                message.text = MESSAGE;
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
        if (message.text == MESSAGE)
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