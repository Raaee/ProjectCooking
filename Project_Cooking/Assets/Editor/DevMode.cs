using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// A script to help make game design easier. Kinda like a cheat mode system in the editor
/// </summary>
public class DevMode 
{
    /*
     *  
     *  kill all enemies
     *  goose mode, 
     *  spawn enemy, 
     *  freeze/unfreeze all enemies
     *  heal
     */
    [MenuItem("Dev Mode/Commit War Crimes")]
    public static void KillAllEnemies()
    {
        Debug.Log("Commiting War Crimes... Raeus approved");
    }


    [MenuItem("Dev Mode/Toggle Goose Mode")]
    public static void ToggleInvincibility()
    {
       
       var player = GameObject.FindGameObjectWithTag("Player");
        if(!player)
        {
            Debug.Log("dont think theres a player in this scene bub");
        }

        player.GetComponent<Health>().ToggleGodMode();
        Debug.Log("Pete spitting bars: Damage is negated. Goose Mode Activated. Problems mitigated, Answers be created. \n Chaos is abated, Power's reinstated. Raeus be fustrasted, danger is evaded ");
    }

    [MenuItem("Dev Mode/Spawn Random Enemy")]
    public static void SpawnRandomEnemy()
    {
        Debug.Log("plop, heres an enemy");
    }

    [MenuItem("Dev Mode/Toggle Enemy movement")]
    public static void ToggleEnemyMovement()
    {
        Debug.Log("Red light, green light! ");
      
    }
}
