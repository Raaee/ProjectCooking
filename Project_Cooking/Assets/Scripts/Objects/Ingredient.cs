using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Interactable {

    [SerializeField] protected Items item;
    public override void Interact() {
        inventory.AddItem(item);
        // this.gameObject.SetActive(false); 
    }
    public void SetItemType(Items itemType) {
        item = itemType;
    }
    public Items GetItemType() {
        return item;
    }
}
