using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnotherDoorTriggers : MonoBehaviour
{
    public GameObject messageBox;
    public TextMeshProUGUI text;
    public string room;

    private void Start()
    {
        messageBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        messageBox.SetActive(true);
        if (room == "kitchen")
        {
            text.text = "Чем тут пахнет?";
        }
        else
        {
            text.text = "Нам нужна кухня, а не " + room + " комната";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        messageBox.SetActive(false);
    }
}
