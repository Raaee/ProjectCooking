using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour  {

    public List<Items> inventory = new List<Items>();
    private Items currentItem = Items.NONE;

    public void AddItem(Items item) {
        inventory.Add(item);
    }
    public void RemoveItem(Items item) {
        inventory.Remove(item);
    }
    public void ClearInventory() {
        inventory.Clear();
    }
}
