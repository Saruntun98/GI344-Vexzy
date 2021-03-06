using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixerSound;
    public AudioMixer audioMixerEffect;
    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public Toggle isCam;
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int CurrentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string Option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(Option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                CurrentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = CurrentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Screen.SetResolution(640, 480, true, 60);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CamLock()
    {
         if(isCam.isOn)
        {
            Player.instance.isCamera = true;
        }
        if(!isCam.isOn)
        {
            Player.instance.isCamera = false;
        }       
    }

    public void SetVolumeSound(float volume)
    {
        //Debug.Log(volume);
        audioMixerSound.SetFloat("volumeSound", volume);     
    }
    public void SetVolumeEffect(float volume)
    {
        //Debug.Log(volume);   
        audioMixerEffect.SetFloat("volumeEffect", volume);   
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void Quit()
    {
        Application.Quit();
    }

}
