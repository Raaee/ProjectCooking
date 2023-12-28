using com.cyborgAssets.inspectorButtonPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMusic : MonoBehaviour
{

    [SerializeField] private FMODUnity.EventReference dungeonMusic;
    private string Dungeon_Hurt_PARAM_NAME = "DungeonMusicHurt";
    private string ENEMIES_KILLED_PARAM_NAME = "EnemiesKilled";
    private FMOD.Studio.EventInstance instance;
    private int enemiesKilled = 0;
    [SerializeField] private Health playerHealth;
    [SerializeField] private LevelManager levelManager;
   
    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(dungeonMusic);
        playerHealth.OnHurt.AddListener(OnPlayerHurtMusic);
        playerHealth.OnDeath.AddListener(EndMusic);
        levelManager.OnAreaChange.AddListener(PlayMusicLogic);
        
    }

    private void PlayMusicLogic(Current_Area currentArea)
    {
        if (currentArea == Current_Area.DUNGEON)
            StartMusic();
        else
            EndMusic();
    }

    [ProButton]
    public void StartMusic()
    {
        if (playerHealth.IsDead())
            return;
        instance.start();
    }
    [ProButton]
    public void EndMusic()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    [ProButton]
    public void OnPlayerHurtMusic()
    {
        StartCoroutine(hurtMusicEffect());
    }

    private IEnumerator hurtMusicEffect()
    {
        instance.setParameterByName(Dungeon_Hurt_PARAM_NAME, 3);
        yield return new WaitForSeconds(0.5f);
        instance.setParameterByName(Dungeon_Hurt_PARAM_NAME, 3);

    }

    [ProButton]
    public void IncreaseEnemyKilled()
    {
        enemiesKilled++;
        instance.setParameterByName(ENEMIES_KILLED_PARAM_NAME, enemiesKilled);
    }
}
