using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider soundSlider;

    public Toggle screenToggle;

    public TMP_Dropdown qualityDropdown;

    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    float volumeValue;

    void Start()
    {
        LoadValues();
    }

    public void SetVolume(float volume) 
    {
        //audioMixer.SetFloat("volume", volume);

        PlayerPrefs.SetFloat("Music", volume);

        audioMixer.SetFloat("Music", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
    
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    void LoadValues()
    {
        //audioMixer.GetFloat("volume", out volumeValue);
        //soundSlider.value = volumeValue;
        //AudioListener.volume = volumeValue;

        soundSlider.value = PlayerPrefs.GetFloat("Music", 1f);

        screenToggle.isOn = Screen.fullScreen;

        qualityDropdown.value = QualitySettings.GetQualityLevel();

        loadResolutions();
    }

    void loadResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            /*
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
            */
        }
        options.Add("");

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        resolutionDropdown.RefreshShownValue();
    }
}
