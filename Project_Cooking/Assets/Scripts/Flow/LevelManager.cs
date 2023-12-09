using com.cyborgAssets.inspectorButtonPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AreaTimer areaTimer;
    [SerializeField] private Current_Area currentArea;
    [SerializeField] private EnemyManager enemyManager;
   

    [Header("INGREDIENTS SPAWN")]
    public List<GameObject> baseIngredients = new List<GameObject>();
    [SerializeField] private GameObject upperCornerFLoor;
    [SerializeField] private GameObject lowerCornerFLoor;
    [HideInInspector] public UnityEvent<Current_Area> OnAreaChange;

    void Start()    {
        currentArea = Current_Area.LIMBO;
        StartRound();
        areaTimer.OnRoundOver.AddListener(StartRound);
        SpawnAllBaseIngredients();
    }
    public void StartRound() {
        ChangeArea();
       
    }

    [ProButton]
    public void ChangeArea() {
        if (currentArea == Current_Area.DUNGEON) {
            currentArea = Current_Area.KITCHEN;
        } else {
            currentArea = Current_Area.DUNGEON;
          
            enemyManager.SpawnAllEnemies();
        }
        areaTimer.ResetAreaTime(currentArea);
        OnAreaChange?.Invoke(currentArea);
    }
    public void SpawnAllBaseIngredients()
    {
        foreach (GameObject go in baseIngredients)
        {
            float x = Random.Range(upperCornerFLoor.transform.position.x, lowerCornerFLoor.transform.position.x);
            float y = Random.Range(lowerCornerFLoor.transform.position.y, upperCornerFLoor.transform.position.y);
            float z = 0;
            Vector3 randomPos = new Vector3(x, y, z);
            Instantiate(go, randomPos, Quaternion.identity);
        }

    }
    public Current_Area GetCurrentArea() {
        return currentArea;
    }
}
public enum Current_Area {
    LIMBO,
    KITCHEN,
    DUNGEON
}
