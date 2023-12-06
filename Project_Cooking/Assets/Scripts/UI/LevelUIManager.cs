using UnityEngine;
using System.Collections.Generic;

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
    }
    void Update()   {
        UpdateAreaUI();
        UpdateFloor();
    }
    public void UpdateAreaUI()  {
        switch(levelManager.GetCurrentArea()) {
            case Current_Area.LIMBO:
                SetActiveDungeonUI(false);
                SetActiveKitchenUI(false);
                break;
            case Current_Area.DUNGEON:
                SetActiveDungeonUI(true);
                SetActiveKitchenUI(false);
                break;
            case Current_Area.KITCHEN:
                SetActiveDungeonUI(false);
                SetActiveKitchenUI(true);
                break;
        }
    }
    public void SetActiveDungeonUI(bool active) {
        foreach (GameObject go in dungeonUI) {
            go.SetActive(active);
        }
    }
    public void SetActiveKitchenUI(bool active) {
        foreach (GameObject go in kitchenUI) {
            go.SetActive(active);
        }
    }
    public void UpdateFloor() {
        if (levelManager.GetCurrentArea() == Current_Area.DUNGEON) {
            floor.GetComponent<SpriteRenderer>().color = dungeonFloorColor;
        } else {
            floor.GetComponent<SpriteRenderer>().color = kitchenFloorColor;
        }
    }

}
