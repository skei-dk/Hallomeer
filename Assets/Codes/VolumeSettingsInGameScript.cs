using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettingsInGameScript : MonoBehaviour
{

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    AudioSource musicSource;
    AudioSource SFXSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        SFXSource = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        SFXSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    
}
