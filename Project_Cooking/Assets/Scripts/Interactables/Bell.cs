using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bell : MonoBehaviour, IInteractable  {

    [SerializeField] private Cookbook cookbook;
    private SpriteRenderer sr;
    private Inventory playerInventory;

    private bool hasGameWon = false;
    private bool isFunctional = false;

    [Header("VISUAL")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite highlightedSprite;
    
    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        playerInventory = FindObjectOfType<Inventory>();
        
       // HideBell();
    }
    
    public void Interact() {
        Debug.Log("Ding");
        if (isFunctional)
            hasGameWon = CheckIfWon();
        else
            Debug.Log("bell is off clock. try again tomorrow");

        Debug.Log(hasGameWon);
    }
    public bool CheckIfWon() {
        Items winningItem = cookbook.levelRecipe.outputIngredient.item;

        if (playerInventory.inventoryList.Contains(winningItem))
            return true;
        
        
        return false;
    }
    public void ShowBell() {
        this.gameObject.SetActive(true);
        isFunctional = true;
    }
    public void HideBell() {
        this.gameObject.SetActive(false);
    }

    public void NormalSprite() {
        sr.sprite = normalSprite;
    }
    public void HighlightSprite() {
        sr.sprite = highlightedSprite;
    }
    public bool GetHasGameWon() {
        return hasGameWon;
    }

}
