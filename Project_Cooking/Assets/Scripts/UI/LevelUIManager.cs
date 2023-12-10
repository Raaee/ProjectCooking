using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using System.Collections.Generic;
using System;

public class LevelUIManager : MonoBehaviour {

    private LevelManager levelManager;

    [Header("UI")]
    [SerializeField] private GameObject dungeonUI;
    [SerializeField] private GameObject kitchenUI;

    [Header("VISUALS")]
    [SerializeField] private GameObject kitchenInteractables;
    [SerializeField] private GameObject floor;
    [SerializeField] private Color32 dungeonFloorColor;
    [SerializeField] private Color32 kitchenFloorColor;

    private void Start() {
        levelManager = GetComponent<LevelManager>();
        UpdateFloor(Current_Area.DUNGEON);
        levelManager.OnAreaChange.AddListener(UpdateAreaUI);
        levelManager.OnAreaChange.AddListener(UpdateFloor);
        
    }
   
    public void UpdateAreaUI(Current_Area newCurrentAreA)  {
        switch(newCurrentAreA) {
            case Current_Area.LIMBO:
                DisableAllUI();
                break;
            case Current_Area.DUNGEON:            
                SetUpDungeon();
                break;
            case Current_Area.KITCHEN:
                SetUpKitchen();
                break;
        }
    }

    private void DisableAllUI()
    {
        dungeonUI.SetActive(false);
        kitchenUI.SetActive(false);
        kitchenInteractables.SetActive(false);
    }
    [ProButton]
    private void SetUpDungeon()
    {
        dungeonUI.SetActive(true);
        kitchenUI.SetActive(false);
        kitchenInteractables.SetActive(false);
    }
    [ProButton]
    private void SetUpKitchen()
    {
        dungeonUI.SetActive(false);
        kitchenUI.SetActive(true);
        kitchenInteractables.SetActive(true);
    }

    public void UpdateFloor(Current_Area newCurrentAreA) {
        if (newCurrentAreA == Current_Area.DUNGEON) {
            floor.GetComponent<SpriteRenderer>().color = dungeonFloorColor;
        } else {
            floor.GetComponent<SpriteRenderer>().color = kitchenFloorColor;
        }
    }

}
