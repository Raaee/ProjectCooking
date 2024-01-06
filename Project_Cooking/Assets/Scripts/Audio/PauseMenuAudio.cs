using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuAudio : MonoBehaviour
{
    [Header("Slider REF")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [Header("Slider Text")]
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI sfxText;

    private FMOD.Studio.VCA musicVCA;
    private FMOD.Studio.VCA sfxVCA;
    private void Awake() {
        musicSlider.onValueChanged.AddListener(OnMusicVolChange);
        sfxSlider.onValueChanged.AddListener(OnSfxVolChange);
        musicVCA = FMODUnity.RuntimeManager.GetVCA("vca:/MusicVCA");
        sfxVCA = FMODUnity.RuntimeManager.GetVCA("vca:/SFXVCA");
    }
    private void Start() {
        float musicLoadedValue = ES3.Load("musicVol", 75f);
        float sfxLoadedValue = ES3.Load("sfxVol", 75f);

        OnMusicVolChange(musicLoadedValue);
        musicSlider.value = musicLoadedValue;
        OnSfxVolChange(sfxLoadedValue);
        sfxSlider.value = sfxLoadedValue;
    }
    public void OnMusicVolChange(float newVolume) {

        musicText.text = ((int)(newVolume * 100)).ToString();
        musicVCA.setVolume(newVolume);
        ES3.Save("musicVol", newVolume);
    }

    public void OnSfxVolChange(float newVolume) {

        sfxText.text = ((int)(newVolume * 100)).ToString();
        sfxVCA.setVolume(newVolume);
        ES3.Save("sfxVol", newVolume);
    }
}
