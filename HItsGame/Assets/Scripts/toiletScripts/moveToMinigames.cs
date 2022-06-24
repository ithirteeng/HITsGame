using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveToMinigames : MonoBehaviour
{
    public SavedPosition position;
    private void OnTriggerEnter2D(Collider2D col)
    {
        position.initialValue = new Vector3(6.47f, 0f, 0f);
        SceneManager.LoadScene("MinigamesScene");
        
    }
}
