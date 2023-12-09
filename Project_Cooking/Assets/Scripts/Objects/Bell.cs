using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bell : MonoBehaviour, IInteractable  {

    [SerializeField] private Cookbook cookbook;
    private SpriteRenderer sr;
    private Inventory playerInventory;

    [HideInInspector] public UnityEvent OnAllRoundsDone;
    private bool gameWon = false;

    [Header("VISUAL")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite highlightedSprite;
    
    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        playerInventory = FindObjectOfType<Inventory>();
        OnAllRoundsDone.AddListener(ShowBell);
       // HideBell();
    }
    
    public void Interact() {
        Debug.Log("Ding");
        gameWon = CheckIfWon();
    }
    public bool CheckIfWon() {
        Items winningItem = cookbook.levelRecipe.outputIngredient.item;

        if (playerInventory.inventoryList.Contains(winningItem))
            return true;
        
        
        return false;
    }
    public void ShowBell() {
        this.gameObject.SetActive(true);
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

}
