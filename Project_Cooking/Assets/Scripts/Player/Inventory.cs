using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour  {

    [SerializeField] private Actions actions;
    public List<Items> inventory = new List<Items>();
    private Items currentItem = Items.NONE;
    private int invIndex = 0;
    
    private void Awake() {
        actions = GetComponent<Actions>();
        actions.OnItemSelect.AddListener(CurrItemSelected);
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
    public void CurrItemSelected() {
        if (GetComponent<Input>().slotSelect.ReadValue<float>() >= 120f) {
            if ((invIndex + 1) > (inventory.Count - 1)) {
                invIndex = 0;
            } else {
                invIndex++;
            }
        } else {
            if ((invIndex - 1) < 0) {
                invIndex = (inventory.Count - 1);
            }
            else {
                invIndex--;
            }
        }
        currentItem = inventory[invIndex];
        Debug.Log("Item Selected: " + currentItem); 
    }
}
