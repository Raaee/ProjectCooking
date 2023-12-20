using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bell : MonoBehaviour, IInteractable  {

    [SerializeField] private Cookbook cookbook;
    private SpriteRenderer sr;
    private Inventory playerInventory;
    [SerializeField] private FMODUnity.EventReference bellSound;
    private bool hasGameWon = false;
    private bool isFunctional = false;
    public UnityEvent OnGameWon;
    public UnityEvent OnGameLost;

    [Header("VISUAL")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite highlightedSprite;
    
    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        playerInventory = FindObjectOfType<Inventory>();
        
       // HideBell();
    }
    
    public void Interact() {
        PlayBellSound();

        if (!isFunctional) {
            Debug.Log("bell is off clock. try again tomorrow");
            return;
        }

        hasGameWon = CheckIfWon();
        if (hasGameWon) {
            OnGameWon.Invoke();
        } else {
            OnGameLost.Invoke();
        }
        
    }
    public bool CheckIfWon() {
        Items winningItem = cookbook.levelRecipe.outputIngredient.item;

        if (playerInventory.inventoryList.Contains(winningItem))
            return true;
        
        
        return false;
    }
    public void ShowBell() {
        // sfx when bell is functional here
        this.gameObject.SetActive(true);
        isFunctional = true;
    }
    public void HideBell() {
        this.gameObject.SetActive(false);
        isFunctional = false;
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
    private void PlayBellSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(bellSound, this.transform.position);
    }
}
