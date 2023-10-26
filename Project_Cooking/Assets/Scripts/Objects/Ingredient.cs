using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Interactable {

    [SerializeField] private Items item;
    [SerializeField] private bool canInteract;
    [SerializeField] private bool isItem;
    private Inventory inventory;

    private void Awake() {
        inventory = FindObjectOfType<Inventory>();
    }
    public override bool CanInteract() {
        return canInteract;
    }
    public override bool IsItem() {
        return isItem;
    }
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
