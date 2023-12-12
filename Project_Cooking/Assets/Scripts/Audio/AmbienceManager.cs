using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    [SerializeField] private LevelManager lvlManager;
    [SerializeField] private FMODUnity.EventReference dungeonAmbi;
    [SerializeField] private FMODUnity.EventReference kitchenAmbi;
    [SerializeField] private FMODUnity.EventReference dungeonSpecial;
    private FMOD.Studio.EventInstance d_instance;
    private FMOD.Studio.EventInstance k_instance;
    private void Start()
    {
        lvlManager.OnAreaChange.AddListener(PlayAmbienceLogic);
        d_instance = FMODUnity.RuntimeManager.CreateInstance(dungeonAmbi);
        k_instance = FMODUnity.RuntimeManager.CreateInstance(kitchenAmbi);
    }

    private void PlayAmbienceLogic(Current_Area currentArea)
    {
        if(currentArea == Current_Area.KITCHEN)
        {
            k_instance.start();
            d_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else
        {
            d_instance.start();
            PlayRandomSparkleSFX();
            k_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void PlayRandomSparkleSFX()
    {
        StartCoroutine(playSfx());
    }
    private IEnumerator playSfx()
    {
        float randDelay = UnityEngine.Random.Range(0.01f, 3f);
        yield return new WaitForSeconds(randDelay);
        FMODUnity.RuntimeManager.PlayOneShot(dungeonSpecial, this.transform.position);
    }
}
