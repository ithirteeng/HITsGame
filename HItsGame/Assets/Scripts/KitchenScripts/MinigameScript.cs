using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinigameScript : MonoBehaviour
{
    public Slider slider;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            slider.value += 25;
            if (slider.value >= 100)
            {
                SceneManager.UnloadSceneAsync("MinigameScene");
                PlayerAppearance.player.SetActive(true);
            }
        }
    }
}