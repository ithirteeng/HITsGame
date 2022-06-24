using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class toiletScript : MonoBehaviour
{
    public static bool ifPlayedInEgg = false;
    public Collider2D trigger;
    private bool isTriggerEnabled = false;

    private void Start()
    {
        trigger.enabled = false;
    }

    private void Update()
    {
        if (ifPlayedInEgg && !isTriggerEnabled)
        {
            trigger.enabled = true;
        }
    }
}
