using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuAudioControls : MonoBehaviour
{
   // private int playerAudioAmt = 0;
    //private const int MAX_AUDIO_AMOUNT = 100;
    [Header("AUDIO REF")]
    [SerializeField] private FMODUnity.EventReference passingNodeAudio;//1
    [SerializeField] private FMODUnity.EventReference uiAccept;//2
    [SerializeField] private FMODUnity.EventReference uiHover;//...
    [SerializeField] private FMODUnity.EventReference bellRing;
    [SerializeField] private FMODUnity.EventReference openBook;
    [SerializeField] private FMODUnity.EventReference closeBook;
    [SerializeField] private FMODUnity.EventReference musicSamples;
    private FMODUnity.EventReference playedAudio;
    [Header("AUDIO REF Ambi")]
    [SerializeField] private FMODUnity.EventReference dungeonAmbi;
    [SerializeField] private FMODUnity.EventReference dungeonSpecial;
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance instance2;
    [Header("Slider REF")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [Header("Slider Text")]
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI sfxText;
    private float timer = 0f;
    private float delay = 4.5f;

    private FMOD.Studio.VCA musicVCA;
    private FMOD.Studio.VCA sfxVCA;
    private bool currentlyMovingSlider;

    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(dungeonAmbi);
        instance2 = FMODUnity.RuntimeManager.CreateInstance(dungeonSpecial);
        PlayDungeon();
        musicSlider.onValueChanged.AddListener(OnMusicVolChange);
        sfxSlider.onValueChanged.AddListener(OnSfxVolChange);

        musicVCA = FMODUnity.RuntimeManager.GetVCA("vca:/MusicVCA");
        sfxVCA = FMODUnity.RuntimeManager.GetVCA("vca:/SFXVCA");
        float musicLoadedValue = ES3.Load("musicVol", 0.75f);
        float sfxLoadedValue = ES3.Load("sfxVol", 0.75f);

        OnMusicVolChange(musicLoadedValue);
        musicSlider.value = musicLoadedValue;
        OnSfxVolChange(sfxLoadedValue);
        sfxSlider.value = sfxLoadedValue;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > delay)
        {
            
                PlayRandomMusicTest();
                timer = 0f;
     

            
        }
        
    }
    private void PlayRandomMusicTest()
    {
        FMODUnity.RuntimeManager.PlayOneShot(musicSamples, transform.position);
    }

    //this is specifically for the Unity Button UI stiff
    public void PlayAudio(int audioId)//too lazy to make all the play methods 
    {
        switch (audioId)
        {
            case 1:
                playedAudio = passingNodeAudio;
                break;
            case 2:
                playedAudio = uiAccept;
                break;
            case 3:
                playedAudio = uiHover;
                break;
            case 4:
                playedAudio = bellRing;
                break;
            case 5:
                playedAudio = openBook;
                break;
            case 6:
                playedAudio = closeBook;
                break;
            default:
                Debug.Log("no audio id with this");
                break;
        }

        FMODUnity.RuntimeManager.PlayOneShot(playedAudio, transform.position);
    }

    private void PlayDungeon()
    {
        instance.start();
        instance2.start();
    }

    public void StopDungeon()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance2.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void OnMusicVolChange(float newVolume)
    {

        musicText.text = ((int)(newVolume*100)).ToString();
        musicVCA.setVolume(newVolume);
        ES3.Save("musicVol", newVolume);
    }

    public void OnSfxVolChange(float newVolume)
    {
       
        sfxText.text = ((int)(newVolume * 100)).ToString();
        sfxVCA.setVolume(newVolume);
        ES3.Save("sfxVol", newVolume);
    }

}
