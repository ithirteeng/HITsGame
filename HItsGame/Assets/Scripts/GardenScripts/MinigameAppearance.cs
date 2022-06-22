using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameAppearance : MonoBehaviour
{
    public string minigameScene;
    public Camera camera;
    public GameObject player;
    public GameObject pressECanvas;
    private bool isInTirgger;

    private void Start()
    {
        PlayerAppearance.Init(player, camera);
    }

    private void Update()
    {
        if (isInTirgger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadSceneAsync(minigameScene, LoadSceneMode.Additive);
                player.SetActive(false);
                pressECanvas.SetActive(false);
                camera.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        isInTirgger = true;
        pressECanvas.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        pressECanvas.SetActive(false);
        isInTirgger = false;
    }
}
