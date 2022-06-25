using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoorScript : MonoBehaviour
{
    public static bool isTetrisPassed = false;
    private bool isDoorTriggered = false;
    public Collider2D doorTrigger;
    public GameObject hayStack;

    private void Start()
    {
        hayStack.SetActive(true);
        doorTrigger.enabled = false;
    }

    private void Update()
    {
        if (isTetrisPassed && !isDoorTriggered)
        {
            hayStack.SetActive(false);
            doorTrigger.enabled = true;
        }
    }
}
