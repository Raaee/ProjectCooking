using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IInteractable  {//stove. chopper, mixer, ingredients, cookbook 

    [SerializeField] private Items item;

    private void Awake()
    {
        
    }
    public void Interact() {
        //add the item type to inventory
        Inventory.instance.AddItem(item);
      
        //this.gameObject.SetActive(false); 

    }

    public Items GetItemType() {
        return item;
    }
}
