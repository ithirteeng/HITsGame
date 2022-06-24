using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class minigamesAppearance : MonoBehaviour
{
    public string minigameScene;
    public Camera camera;
    public GameObject player;
    public GameObject pressECanvas;
    private bool isInTirgger;
    [FormerlySerializedAs("mesageCanvas")] public GameObject messageCanvas;
    public TextMeshProUGUI text;
    public string game;

    private void Start()
    {
        messageCanvas.SetActive(false);
        pressECanvas.SetActive(false);
        PlayerAppearance.Init(player, camera);
        
    }

    private void Update()
    {
        if (isInTirgger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // SceneManager.LoadSceneAsync(minigameScene, LoadSceneMode.Additive);
                // player.SetActive(false);
                // pressECanvas.SetActive(false);
                // camera.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        isInTirgger = true;
        messageCanvas.SetActive(true);
        text.text = game;
        pressECanvas.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        messageCanvas.SetActive(false);
        pressECanvas.SetActive(false);
        isInTirgger = false;
    }
}