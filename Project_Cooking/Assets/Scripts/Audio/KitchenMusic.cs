using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenMusic : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference kitchenMusic;
    private string KITCHEN_PROGRESS_PARAM_NAME = "KitchenProgress";
    private string TRANSITION_PROGRESS_PARAM_NAME = "TransitionProgress";
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Cookbook cookbook;
    private FMOD.Studio.EventInstance instance;

    private void Awake()
    {
        levelManager.OnAreaChange.AddListener(MusicLogic);
        cookbook.OnNodeIncreased.AddListener(OnCheckPercentage);
    }
    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(kitchenMusic);
    }

    private void MusicLogic(Current_Area current_Area)
    {
        if (current_Area == Current_Area.KITCHEN)
            StartMusic();
        else
            TransitionToDungeon();
    }

    private void OnCheckPercentage(float percentage)
    {
        if (percentage > 0.333f)
            HalfRecipesUnlocked();
    }

    [ProButton]
    public void StartMusic()
    {
        instance.start();
        instance.setParameterByName(TRANSITION_PROGRESS_PARAM_NAME, 0);
    }

    public void StopMusic()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


    [ProButton]
    public void TransitionToDungeon()
    {
        instance.setParameterByName(TRANSITION_PROGRESS_PARAM_NAME, 6);
    }

    [ProButton]
    public void HalfRecipesUnlocked()
    {
        instance.setParameterByName(KITCHEN_PROGRESS_PARAM_NAME, 6);
    }
}
