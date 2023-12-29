using com.cyborgAssets.inspectorButtonPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMusic : MonoBehaviour
{

    [SerializeField] private FMODUnity.EventReference dungeonMusic;
    private string Dungeon_Hurt_PARAM_NAME = "PlayerHurtInstance";
    private string ENEMIES_KILLED_PARAM_NAME = "EnemiesKilled";
    private string PLAYER_HEALTH_PARAM = "PlayerHealth";
    private string MUSIC_VARIATION_PARAM = "DungeonMusicVariation";
    private string END_MUSIC_PARAM = "EndMusic";
    private FMOD.Studio.EventInstance instance;
    private int enemiesKilled = 0;
    [SerializeField] private Health playerHealth;
    [SerializeField] private LevelManager levelManager;
   
    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(dungeonMusic);
        playerHealth.OnHurt.AddListener(OnPlayerHurtMusic);
        playerHealth.OnDeath.AddListener(EndMusic);
        playerHealth.OnHeal.AddListener(UpdateHealth) ;
        levelManager.OnAreaChange.AddListener(PlayMusicLogic);
        
    }

    private void UpdateHealth()
    {
        instance.setParameterByName(PLAYER_HEALTH_PARAM, playerHealth.GetCurrentHealth()); ;

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
        var randomAmt = UnityEngine.Random.Range(0, 4);
        instance.setParameterByName(MUSIC_VARIATION_PARAM, randomAmt);
        instance.setParameterByName(END_MUSIC_PARAM, 0);
        instance.start();
    }
    [ProButton]
    public void EndMusic()
    {
        instance.setParameterByName(END_MUSIC_PARAM, 1);
      
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
        UpdateHealth();
    }

    [ProButton]
    public void IncreaseEnemyKilled()
    {
        enemiesKilled++;
        instance.setParameterByName(ENEMIES_KILLED_PARAM_NAME, enemiesKilled);
    }
}
