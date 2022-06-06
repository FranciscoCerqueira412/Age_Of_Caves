using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettinsMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    Resolution[] resolutions;

    public Dropdown resol;

    private void Start()
    {
        resolutions=Screen.resolutions;

        resol.ClearOptions();

        List<string> options = new List<string>();

        int currentResol = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResol = i;
            }
        }

        resol.AddOptions(options);
        resol.value = currentResol;
        resol.RefreshShownValue();
    }
    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("volume", volume);

    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullscreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }

    public void SetResolution(int resolIndex)
    {
        Resolution resolution = resolutions[resolIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
