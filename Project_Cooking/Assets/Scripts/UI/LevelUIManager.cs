using UnityEngine;
using System.Collections.Generic;
using System;

public class LevelUIManager : MonoBehaviour {

    private LevelManager levelManager;

    [Header("UI")]
    [SerializeField] private List<GameObject> dungeonUI = new List<GameObject>();
    [SerializeField] private List<GameObject> kitchenUI = new List<GameObject>();

    [Header("VISUALS")]
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
                SetUpDungeonUI();
                break;
            case Current_Area.KITCHEN:
                SetUpKitchenUI();
                break;
        }
    }

    private void DisableAllUI()
    {
        foreach (GameObject go in dungeonUI)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in kitchenUI)
        {
            go.SetActive(false);
        }
    }

    private void SetUpDungeonUI()
    {
        foreach (GameObject go in dungeonUI)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in kitchenUI)
        {
            go.SetActive(false);
        }
    }

    private void SetUpKitchenUI()
    {
        foreach (GameObject go in dungeonUI)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in kitchenUI)
        {
            go.SetActive(true);
        }
    }

    public void UpdateFloor(Current_Area newCurrentAreA) {
        if (newCurrentAreA == Current_Area.DUNGEON) {
            floor.GetComponent<SpriteRenderer>().color = dungeonFloorColor;
        } else {
            floor.GetComponent<SpriteRenderer>().color = kitchenFloorColor;
        }
    }

}
