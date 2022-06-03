using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movementBetweenScenes : MonoBehaviour
{
    public String sceneName;
    public SavedPosition position;
    public Vector3 nextPosition;
    private void OnTriggerStay2D(Collider2D collider)
    {
        position.initialValue = nextPosition;
        SceneManager.LoadScene(sceneName);
    }
}
