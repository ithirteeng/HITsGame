using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAppearance : MonoBehaviour
{
    public GameObject portal;
    private bool _isTrigger;
    private float _timer;
    private float _waitTime = 1f;

    private void Start()
    {
        portal.SetActive(false);
    }

    private void Update()
    {
        if (_isTrigger)
        {
            _timer += Time.deltaTime;
            if (_timer > _waitTime)
            {
                PortalAppearanceFunction();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        _timer = 0f;
        _isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _timer = 0f;
        _isTrigger = false;
    }

    private void PortalAppearanceFunction()
    {
        portal.SetActive(true);
    }
}
