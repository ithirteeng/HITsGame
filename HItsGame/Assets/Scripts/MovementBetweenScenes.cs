using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementBetweenScenes : MonoBehaviour
{
    public String sceneName;
    public SavedPosition position;
    public Vector3 nextPosition;
    private float _waitTime = 1f;
    private float _timer;
    private bool _isTrigger;


    private void Update()
    {
        if (_isTrigger)
        {
            _timer += Time.deltaTime;
            if (_timer > _waitTime)
            {
                MoveBetweenScenes();
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

    private void MoveBetweenScenes()
    {
        position.initialValue = nextPosition;
        SceneManager.LoadScene(sceneName);
    }
}