using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SignsSccript : MonoBehaviour
{
    public GameObject message;
    public TextMeshProUGUI sign;
    public string signName;
    static bool wasInChicken = false;

    private void Start()
    {
        message.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (signName == "toilet")
        {
            sign.text = "О, туалет, было бы неплохо туда сходить";
        }
        else
        {
            if (!wasInChicken)
            {
                sign.text = "Опа, курятник, то что нам нужно";
            }
            else
            {
                sign.text = "Опять курятник?";
            }
            
        }
        message.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        message.SetActive(false);
    }
}
