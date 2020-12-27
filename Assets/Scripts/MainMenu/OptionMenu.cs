using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer mixer;

    public TMP_Dropdown resDropdown;
    public TMP_Dropdown qualityDropDown;
    public Slider volumeSlider;

    float volumeLevel;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i=0;i<resolutions.Length;i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + "@" + resolutions[i].refreshRate;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resDropdown.AddOptions(options);
        resDropdown.value = currentResolutionIndex;
        resDropdown.RefreshShownValue();
        qualityDropDown.value = QualitySettings.GetQualityLevel();
        mixer.GetFloat("Volume",out volumeLevel);
        volumeSlider.value = volumeLevel;
    }

    public void SetVolume(float vol)
    {
        mixer.SetFloat("Volume", vol);
    }

    public void ToggleFullScreen(bool val)
    {
        Screen.fullScreen = val;
    }

    public void ChangeQualityLevel(int level)
    {
        print(QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(level);
    }

    public void ChangeResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen); 
    }
}
