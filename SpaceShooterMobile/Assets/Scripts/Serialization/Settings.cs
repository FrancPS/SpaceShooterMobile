using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle toggleEasy;
    public Toggle toggleMedium;
    public Toggle toggleDifficult;

    void Start()
    {
        volumeSlider.value = SettingsManager.Inst.stData.volumeLevel;
        switch (SettingsManager.Inst.stData.difficulty)
        {
            case 2:
                toggleDifficult.isOn = true;
                break;
            case 1:
                toggleMedium.isOn = true;
                break;
            case 0:
            default:
                toggleEasy.isOn = true;
                break;
        }
    }

    public void UpdateParameters()
    {
        SettingsManager.Inst.stData.volumeLevel = volumeSlider.value;
        AudioListener.volume = volumeSlider.value;
    }

    public void SetDifficulty(int mode)
    {
        SettingsManager.Inst.stData.difficulty = mode;
    }
}
