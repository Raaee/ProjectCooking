using com.cyborgAssets.inspectorButtonPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AreaTimer areaTimer;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private Cookbook cookbook;
    [SerializeField] private Bell bell;

    [Header("INGREDIENTS SPAWN")]
    public List<GameObject> baseIngredients = new List<GameObject>();
    [SerializeField] private GameObject upperCornerFLoor;
    [SerializeField] private GameObject lowerCornerFLoor;
    [HideInInspector] public UnityEvent<Current_Area> OnAreaChange;

    [Header("DEBUG")]
    [SerializeField] private Current_Area currentArea;
    [SerializeField] private int amtOfRound = 6;

    void Start()    {
        amtOfRound = cookbook.levelRecipe.recipeSteps.Count;
        currentArea = Current_Area.LIMBO;
        StartLevel();
        areaTimer.OnRoundOver.AddListener(StartLevel);
        SpawnAllBaseIngredients();
    }
    public void StartLevel() {

        if (amtOfRound >= 1) {
            ChangeArea();
        }

        if (amtOfRound == 0) {
            EndLevel();
        }
    }
    public void EndLevel() {
        bell.ShowBell();
        areaTimer.PauseTimer();
    }

    [ProButton]
    public void ChangeArea() {
        if (currentArea == Current_Area.DUNGEON) {
            currentArea = Current_Area.KITCHEN;
            amtOfRound--;
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
