using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public abstract class Workstation : MonoBehaviour {

    [SerializeField] protected List<WorkstationRecipe> workstationRecipesSO;
    [SerializeField] protected Slider progressBar;
    [SerializeField] protected GameObject progBarCanvas;
    [SerializeField] protected WorkstationSO workstationSO;
    private Actions actions;
    protected bool hasAllIngredients = true;
    protected Items outputIngredient;
    protected SpriteRenderer sr;

    [Header("DEBUG")]
    [SerializeField] protected InteractProgressState progressState;
    [SerializeField] protected bool isCharging;
    private const float PROGRESS_RATE = 1.0f;
    public abstract void OnInteractionComplete();
    
    private void Awake() {
        progressState = InteractProgressState.IDLE;
        sr = GetComponentInChildren<SpriteRenderer>();
        actions = FindObjectOfType<Actions>();
        actions.OnInteractHeld_Cancelled.AddListener(UnCharge);
        progBarCanvas.SetActive(false);
    }
    private void Update() {
        ProgressBarStateMachine();
    }
    public void AddOutputFromInteraction() {
        outputIngredient = Items.CHARCOAL;

        if (Inventory.instance.IsEmpty()) {
            Debug.LogWarning("Your inventory is empty.");
            return;
            //This is for when the player interacts with a workstation while their inventory is empty:
            //This is to prevent the production of coal farming.
        }

        CheckIfInventoryHasAll();
        Inventory.instance.ClearInventory();
        Inventory.instance.AddItem(outputIngredient);
    }
    public void CheckIfInventoryHasAll() {
        foreach (WorkstationRecipe recipe in workstationRecipesSO) {
            if (recipe.workstationInput.All(IngredientSO => Inventory.instance.inventoryList.Contains(IngredientSO.item))) {
                outputIngredient = recipe.workstationOutput.item;
            }
        }
    }
    private void ProgressBarStateMachine()
    {
        switch (progressState)
        {
            case InteractProgressState.IDLE:
                progressBar.value = progressBar.minValue;
                if (isCharging) {
                    progressState = InteractProgressState.INCREASING;
                }
                break;

            case InteractProgressState.INCREASING:
                if (progressBar.value >= progressBar.maxValue)  {
                    progressState = InteractProgressState.FULL;
                }
                if (!isCharging)    {
                    progressState = InteractProgressState.DECREASING;
                }
                else {
                    progressBar.value += PROGRESS_RATE * Time.deltaTime;
                }
                break;

            case InteractProgressState.DECREASING:
                if (progressBar.value <= progressBar.minValue)  {
                    progressState = InteractProgressState.IDLE;
                }
                if (isCharging) {
                    progressState = InteractProgressState.INCREASING;
                }
                else {
                    progressBar.value -= PROGRESS_RATE * Time.deltaTime;
                }
                break;

            case InteractProgressState.FULL:
                OnInteractionComplete();
                progressState = InteractProgressState.IDLE;
                isCharging = false;
                break;
        }
    }
    public void RemoveChargeListener() {
        actions.OnInteractHeld_Started.RemoveListener(Charge);
    }
    public void AddChargeListener() {
        actions.OnInteractHeld_Started.AddListener(Charge);
    }
    public void Charge() {
        isCharging = true;
    }
    public void UnCharge() {
        isCharging = false;
    }
    public void HighlightSprite() {
        sr.sprite = workstationSO.highlightedSprite;
        progBarCanvas.SetActive(true);
    }
    public void NormalSprite() {
        sr.sprite = workstationSO.normalSprite;
        progBarCanvas.SetActive(false);
    }
}

public enum InteractProgressState { 
    IDLE,
    INCREASING,
    DECREASING,
    FULL
}
