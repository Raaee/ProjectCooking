using com.cyborgAssets.inspectorButtonPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private AreaTimer areaTimer;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private Cookbook cookbook;
    [SerializeField] private Bell bell;
    [SerializeField] private GameObject playerObj;
    private bool hasGameWon;

    [Header("AREA SWITCHING")]
    [SerializeField] private GameObject kitchenTeleportLoc;
    [SerializeField] private GameObject dungeonTeleportLoc;

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
        
        //BAD CODE ALERT
        //we want to call the below event only inbetween fading, fade manager will do it
       // OnAreaChange?.Invoke(currentArea);
        int NegativeNumberToRepresentNullSceneHiRaeus = -1;
        FadeManager.instance?.FadeOutAndLoadScene(NegativeNumberToRepresentNullSceneHiRaeus);//-1 means it wont fade to a new scene 
    }
    //Fade manager will look for this and call it 
    public void InvokeOnAreaChange()
    {
        if (currentArea == Current_Area.DUNGEON)
        {
            currentArea = Current_Area.KITCHEN;
            playerObj.transform.position = kitchenTeleportLoc.transform.position;
            amtOfRound--;
        }
        else
        {
            currentArea = Current_Area.DUNGEON;
            playerObj.transform.position = dungeonTeleportLoc.transform.position;
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
            float z = 0.5f;
            Vector3 randomPos = new Vector3(x, y, z);
            GameObject baseIngredient = Instantiate(go, randomPos, Quaternion.identity);
            baseIngredient.transform.parent = FindObjectOfType<Cookbook>().transform;
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
