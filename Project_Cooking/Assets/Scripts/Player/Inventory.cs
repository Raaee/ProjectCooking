using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour  {

    private Actions actions;
    private Input input;
    public List<Items> inventory = new List<Items>();
    private Items currentItem = Items.NONE;
    private int invIndex = 0;
    private const float SCROLL_THRESHOLD = 120f;
    [SerializeField] private int MAX_INV_SPACES = 3;
    private void Awake() {
        actions = GetComponent<Actions>();
        input = GetComponent<Input>();
        actions.OnItemSelect.AddListener(CurrItemSelectedFromScroll);
        actions.OnItemDrop.AddListener(CurrItemDropped);
    }

    public void AddItem(Items item) {
        inventory.Add(item);
    }
    public void RemoveItem(Items item) {
        inventory[invIndex] = Items.NONE;
        Debug.Log("**** ITEM DROPPED: " + currentItem);
    }
    public void ClearInventory() {
        inventory.Clear();
    }
    public void CurrItemDropped() {
        RemoveItem(currentItem);
    }
    public void CurrItemSelectedFromScroll() {
        // Scroll UP:
        if (input.slotSelect.ReadValue<float>() >= SCROLL_THRESHOLD) {
            invIndex++;
            if (invIndex >= MAX_INV_SPACES) {
                invIndex = 0;
            }
        }

        // Scroll DOWN:
        if (input.slotSelect.ReadValue<float>() < SCROLL_THRESHOLD) {
            invIndex--;
            if (invIndex < 0) {
                invIndex = MAX_INV_SPACES - 1;
            }
        }

        currentItem = inventory[invIndex];
        Debug.Log("Item Selected: " + currentItem); 
    }
    public Items GetCurrentItem() {
        return currentItem;
    }
    public int GetCurrentItemIndex() {
        return invIndex;
    }
}
