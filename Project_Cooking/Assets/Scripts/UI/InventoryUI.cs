using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    [SerializeField] private Inventory inventory;
    private Items currentItem;
    private List<Image> images = new List<Image>();
    private int currItemIndex = 0;
    public void UpdateSelected() {
        currItemIndex = inventory.GetCurrentItemIndex();
        ShowItemSelected();
    }
    public void ShowItemSelected() {
        DisableAllHighlighted();
        images[currItemIndex].enabled = true;
    }
    private void DisableAllHighlighted() {
        // this is to disable all the highlighted slots for when the player changes current item with scroll
    }
}
