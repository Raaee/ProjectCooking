using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IInteractable  {

    [SerializeField] private Items item;
    public void Interact() {
        //add the item type to inventory
        Debug.Log("Interact called ?");
        bool itemAdded = Inventory.instance.AddItem(item);
        if (itemAdded) {
            Destroy(this.gameObject);
        }
    }

    public Items GetItemType() {
        return item;
    }
}
