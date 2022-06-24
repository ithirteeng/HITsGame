using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalMoveToDargen : MonoBehaviour
{
    public SavedPosition position;
   
    private void OnTriggerEnter2D(Collider2D col)
    {
        //animator.SetTrigger("Fade");
        SceneManager.LoadScene("BarnScene");
        position.initialValue = new Vector3(0.37f, 3.82f, -0.1676572f);
    }
}
