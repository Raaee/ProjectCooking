using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IInteractable {

    [SerializeField] private Items item;
    [SerializeField] private bool canInteract;
    [SerializeField] private bool isItem;

    public bool CanInteract() {
        return canInteract;
    }
    public bool IsItem() {
        return isItem;
    }
    public void Interact() {
        Debug.Log(item);        
    }
    public void SetItemType(Items itemType) {
        item = itemType;
    }
    public Items GetItemType() {
        return item;
    }
}
