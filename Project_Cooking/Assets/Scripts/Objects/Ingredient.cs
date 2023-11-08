using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IInteractable  {

    [SerializeField] private IngredientSO ingredientSO;
    [SerializeField] private Items item;
    private SpriteRenderer sr;
    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }
    public void Interact() {
        //add the item type to inventory
        Debug.Log("Interact called ?");
        bool itemAdded = Inventory.instance.AddItem(item);
        if (itemAdded) {
            Destroy(this.gameObject);
        }
    }
    public void HighlightSprite() {
        sr.sprite = ingredientSO.highlightedSprite;
    }
    public void NormalSprite() {
        sr.sprite = ingredientSO.normalSprite;
    }

    public Items GetItemType() {
        return item;
    }
}
