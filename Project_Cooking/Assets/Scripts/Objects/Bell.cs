using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour, IInteractable  {

    [SerializeField] private Cookbook cookbook;

    [Header("VISUAL")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite highlightedSprite;
    private SpriteRenderer sr;
    private Inventory playerInventory;
    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        playerInventory = FindObjectOfType<Inventory>();
    }
    
    public void Interact() {
        Debug.Log("Ding");
        Debug.Log(CheckIfWon());
    }
    public bool CheckIfWon() {
        Items winningItem = cookbook.levelRecipe.outputIngredient.item;

        if (playerInventory.inventoryList.Contains(winningItem))
            return true;
        
        
        return false;
    }

    public void NormalSprite() {
        sr.sprite = normalSprite;
    }
    public void HighlightSprite() {
        sr.sprite = highlightedSprite;
    }

}
