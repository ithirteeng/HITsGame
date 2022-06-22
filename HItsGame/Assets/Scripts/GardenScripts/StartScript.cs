using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    public GameObject pressEMessage;
    public GameObject text;
    public TextMeshProUGUI message;
    public Collider2D collider2D;

    private void Start()
    {
        pressEMessage.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        message.text = "Жеесть, как я на ферме оказался? Ну да ладно, пойду похаваю";
        text.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        text.SetActive(false);
        collider2D.enabled = false;
    }
}