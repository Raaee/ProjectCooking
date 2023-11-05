using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IInteractable  {

    [SerializeField] private Items item;
    public void Interact() {
        //add the item type to inventory
        if (Inventory.instance.AddItem(item)) {
            Destroy(this.gameObject);
        }
    }

    public Items GetItemType() {
        return item;
    }
}
