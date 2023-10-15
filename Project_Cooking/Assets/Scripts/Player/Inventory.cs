using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour  {

    [SerializeField] private List<Items> inventory = new List<Items>();
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
public enum Items {
    FLOUR,
    DOUGH,
    BREAD,
    TOMATO,
    SLICED_TOMATO,
    TOMATO_PASTE,
    CHEESE,
    SHREDDED_CHEESE,
    LETTUCE,
    CHOPPED_LETTUCE,
    EGG,
    MILK,
    BATTER,
    SANDWICH,
    PANCAKE,
    PIZZA,
    SALAD,
    CHARCOAL,
    KNIFE,
    NONE
}