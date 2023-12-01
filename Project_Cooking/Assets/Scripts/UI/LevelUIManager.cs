using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIManager : MonoBehaviour {

    [SerializeField] private AreaTimer areaTimer;
    [SerializeField] private GameObject inventoryUI_GO;
    [SerializeField] private GameObject bloodBarUI_GO;
    [SerializeField] private GameObject healthUI_GO;
    [SerializeField] private GameObject abilitiesUI_GO;

    void Update()   {
        UpdateAreaUI();
    }
    public void UpdateAreaUI() {
        switch(areaTimer.GetCurrentArea()) {
            case Current_Area.LIMBO:
                UIGoActive(false, false, false, false);
                break;
            case Current_Area.DUNGEON:
                UIGoActive(false, true, true, true);
                break;
            case Current_Area.KITCHEN:
                UIGoActive(true, false, false, false);
                break;
        }
    }
    public void UIGoActive(bool inv, bool blood, bool health, bool ability) {
        inventoryUI_GO.SetActive(inv);
        bloodBarUI_GO.SetActive(blood);
        healthUI_GO.SetActive(health);
        //abilitiesUI_GO.SetActive(ability);
    }
}
