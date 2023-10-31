using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// A script to help make game design easier. Kinda like a cheat mode system in the editor
/// </summary>
public class DevMode 
{
    private const string peteSpittingBars = "Damage is negated. Goose Mode Activated. Problems mitigated, Answers be created. \n Chaos is abated, Power's reinstated. Raeus be fustrasted, danger is evaded ";
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
        var enemies = GameObject.FindObjectsOfType<EnemyStateHandler>();
        foreach(EnemyStateHandler enemy in enemies)
        {
           var enemyHealth = enemy.gameObject.GetComponent<Health>();
            enemyHealth.InstaKill();
        }

    }


    [MenuItem("Dev Mode/Toggle Goose Mode")]
    public static void ToggleInvincibility()
    {
       
       var player = GameObject.FindGameObjectWithTag("Player");
        if(!player)
        {
            Debug.Log("dont think theres a player in this scene bub");
            return;
        }

        player.GetComponent<Health>().ToggleGodMode();
        Debug.Log(peteSpittingBars);
    }

    [MenuItem("Dev Mode/Spawn Random Enemy")]
    public static void SpawnRandomEnemy()
    {
        Debug.Log("plop, heres an enemy");
        //find player and get its position 
        var player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.Log("dont think theres a player in this scene bub");
            return;
        }
        Vector2 playerPos = player.transform.position;
        //do a random point on a radius around player

        //get enemy manager and spawn an enemy randomly in a radius around the player
        EnemyManager enemyManager = GameObject.FindObjectOfType<EnemyManager>();
        if (!enemyManager)
        {
            Debug.Log("dont think theres a enemyMaanager in this scene bub");
            return;
        }
       float radius = 10f;
       Vector3 randomPoint = Random.onUnitSphere * radius;
       enemyManager.SpawnEnemyAtPoint(randomPoint);

    }

    [MenuItem("Dev Mode/Toggle Enemy movement")]
    public static void ToggleEnemyMovement()
    {
        Debug.Log("Red light, green light! ");      
    }
}
