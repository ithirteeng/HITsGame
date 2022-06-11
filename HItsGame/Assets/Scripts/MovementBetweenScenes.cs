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
    public GameObject preesEText;
    private bool _IsTnTrigger = false;

    private void Start()
    {
        preesEText.SetActive(false);
    }

    private void Update()
    {
        if (_IsTnTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MoveBetweenScenes();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        _IsTnTrigger = true;
        preesEText.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _IsTnTrigger = false;
        preesEText.SetActive(false);
    }

    private void MoveBetweenScenes()
    {
        position.initialValue = nextPosition;
        SceneManager.LoadScene(sceneName);
    }
}