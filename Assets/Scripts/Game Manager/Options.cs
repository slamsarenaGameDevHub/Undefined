using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioMixer masterMixer;
    float sfxVol;
    float musicVol;
    float masterVol;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider masterSlider;

    [Header("Graphics")]
    [SerializeField] Toggle screenToggle;
    [SerializeField] TMP_Dropdown qualityDropdown;


    private void OnEnable()
    {
        AssignValues();
       
    }
    void AssignValues()
    {
        // Graphics
        switch (Screen.fullScreen)
        {
            case true:
                screenToggle.isOn = true;
                break;
            case false:
                screenToggle.isOn = false;
                break;
        }

        switch (QualitySettings.GetQualityLevel())
        {
            case 0:
                qualityDropdown.value = 0;
                break;
            case 1:
                qualityDropdown.value = 1;
                break;
            case 2:
                qualityDropdown.value = 2;
                break;
            case 3:
                qualityDropdown.value = 3;
                break;
        }


        //Sound
        masterMixer.GetFloat("SFX",out sfxVol);
        masterMixer.GetFloat("Music",out musicVol);
        masterMixer.GetFloat("Music",out masterVol);

        sfxSlider.value = sfxVol;
        musicSlider.value = musicVol;
        masterSlider.value = masterVol;
    }

    public void SetSfx(float sfx)
    {
        masterMixer.SetFloat("SFX", sfx);
    }
    public void SetMusic(float music)
    {
        masterMixer.SetFloat("Music", music);
    }
    public void SetMaster(float master)
    {
        masterMixer.SetFloat("Master", master);
    }
    public void setFullscreen(bool full)
    {
        Screen.fullScreen = full;
    }
    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
}
