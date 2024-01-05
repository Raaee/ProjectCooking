using com.cyborgAssets.inspectorButtonPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

    [Header("REFERENCES")]
    [SerializeField] private AreaTimer areaTimer;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private Cookbook cookbook;
    [SerializeField] private Bell bell;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private ProgressBar bloodBar;
    [SerializeField] private Inventory inv;
    [SerializeField] private Health playerHealth;

    private bool hasGameWon;

    [Header("AREA SWITCHING")]
    [SerializeField] private GameObject kitchenTeleportLoc;
    [SerializeField] private GameObject dungeonTeleportLoc;

    [Header("INGREDIENTS SPAWN")]
    public List<GameObject> baseIngredients = new List<GameObject>();
    public List<GameObject> ingredientsInKitchen = new List<GameObject>();
    [SerializeField] private GameObject upperCornerFLoor;
    [SerializeField] private GameObject lowerCornerFLoor;
    [HideInInspector] public UnityEvent<Current_Area> OnAreaChange;

    [Header("DEBUG")]
    [SerializeField] private Current_Area currentArea;
    [SerializeField] private int amtOfRound = 6;

    void Start()    {
        Init();
        areaTimer.OnRoundOver.AddListener(ChangingLevel);
    }
    private void Init() {
        if (instance != null && instance != this) {
            Destroy(this);
        }
        else {
            instance = this;
           
        }
        StartLevel();
    }
    public void StartLevel() {
        ResetLevel();
        amtOfRound = AllRecipeData.instance.levelRecipe.recipeSteps.Count;
        currentArea = Current_Area.LIMBO;
        // ChangeArea();
        InvokeOnAreaChange();
        SpawnAllBaseIngredients();
    }
    public void ResetLevel() {
        playerHealth.ResetHealth();
        inv.ClearInventory();
        MoveToDungeon();
        bloodBar.ResetBar();
        RemoveAllIngredientsFromKitchen();
        enemyManager.SetAmtEnemiesPerRound(3);
    }
    public void ChangingLevel() {

        if (amtOfRound > 0) {
           
            ChangeArea();
            //Debug.LogError("changing area");
        }

        if (amtOfRound == 1 && currentArea == Current_Area.DUNGEON) {
            EndLevel();
        }

        if (amtOfRound == 0)
        {
            var winPanel = FindObjectOfType<WinningPanelUI>();

            //insta lose if the winning panel is never shown
            if(!winPanel.playerWon)
                FindObjectOfType<DeathPanelUI>().ShowDeathPanel();
        }
    }
    [ProButton]
    public void EndLevel() {

        bell.ShowBell();        
        areaTimer.PauseTimer();
        areaTimer.ActivateVignette(false);
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

            MoveToKitchen();
            amtOfRound--;
          
        }
        else
        {

            int enemyIncrementAmtToImproveGameDesign = 1;
            MoveToDungeon();
            if (amtOfRound % 2 == 0) {
                enemyManager.DecrementBloodDropAmt(enemyIncrementAmtToImproveGameDesign);
            }
            enemyManager.IncrementAmtEnemies(enemyIncrementAmtToImproveGameDesign);
            enemyManager.SpawnAllEnemies();
            playerHealth.StartInvincibility();
        }
        areaTimer.ResetAreaTime(currentArea);
        OnAreaChange?.Invoke(currentArea); // DO NOT MOVE IT WILL EXPLODE;; LIVE BOMB
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
            ingredientsInKitchen.Add(baseIngredient);
        }

    }
    public void RemoveIngredientFromKitchen(GameObject go) {
        ingredientsInKitchen.Remove(go);
    }
    public void AddIngredientToKitchen(GameObject go) {
        ingredientsInKitchen.Add(go);
    }
    public void RemoveAllIngredientsFromKitchen() {
        ingredientsInKitchen.Clear();
        for (int i = 0; i < cookbook.transform.childCount; i++) {
            Destroy(cookbook.transform.GetChild(i).gameObject);
        }
    }
    public Current_Area GetCurrentArea() {
        return currentArea;
    }     
    public void MoveToDungeon() {
        currentArea = Current_Area.DUNGEON;
        playerObj.transform.position = dungeonTeleportLoc.transform.position;
    }
    public void MoveToKitchen() {
        currentArea = Current_Area.KITCHEN;
        playerObj.transform.position = kitchenTeleportLoc.transform.position;
    }
}
public enum Current_Area {
    LIMBO,
    KITCHEN,
    DUNGEON
}
