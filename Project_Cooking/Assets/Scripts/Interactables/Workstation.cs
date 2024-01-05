using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public abstract class Workstation : MonoBehaviour
{

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

    [Header("AUDIO")]
    [SerializeField] private FMODUnity.EventReference workstationOnCompleteSound;
    [SerializeField] private FMODUnity.EventReference workstationFailed;
    [Header("VFX")]
    [SerializeField] private ParticleSystem successParticle;
    [SerializeField] private ParticleSystem failParticle;
    private int emissionCount = 10;
    public abstract void OnInteractionComplete();

    private void Awake()
    {
        progressState = InteractProgressState.IDLE;
        sr = GetComponentInChildren<SpriteRenderer>();
        actions = FindObjectOfType<Actions>();
        actions.OnInteractHeld_Cancelled.AddListener(UnCharge);
        progBarCanvas.SetActive(false);
    }
    private void Update()
    {
        ProgressBarStateMachine();
    }
    public void AddOutputFromInteraction()
    {
        outputIngredient = Items.CHARCOAL;

        if (Inventory.instance.IsEmpty())
        {
            Debug.LogWarning("Your inventory is empty.");
            return;
            //This is for when the player interacts with a workstation while their inventory is empty:
            //This is to prevent the production of coal farming.
        }
        
        CheckIfInventoryHasAll();
        if (outputIngredient == Items.CHARCOAL)
            failParticle?.Emit(emissionCount);
        else
            successParticle?.Emit(emissionCount);
        Inventory.instance.ClearInventory();
        Inventory.instance.AddItem(outputIngredient);
    }
    public void CheckIfInventoryHasAll()
    {
        foreach (WorkstationRecipe recipe in workstationRecipesSO)
        {
            if (recipe.workstationInput.All(IngredientSO => Inventory.instance.inventoryList.Contains(IngredientSO.item)))
            {
                outputIngredient = recipe.workstationOutput.item;
                return;
            }
        }
        //if we reach here we failed and its time to sizzle 
        PlayOnFailSound();
        
    }
    private void ProgressBarStateMachine()
    {
        switch (progressState)
        {
            case InteractProgressState.IDLE:
                progressBar.value = progressBar.minValue;
                if (isCharging)
                {
                    progressState = InteractProgressState.INCREASING;
                }
                break;

            case InteractProgressState.INCREASING:
                if (progressBar.value >= progressBar.maxValue)
                {
                    progressState = InteractProgressState.FULL;
                }
                if (!isCharging)
                {
                    progressState = InteractProgressState.DECREASING;
                }
                else
                {
                    progressBar.value += PROGRESS_RATE * Time.deltaTime;
                }
                break;

            case InteractProgressState.DECREASING:
                if (progressBar.value <= progressBar.minValue)
                {
                    progressState = InteractProgressState.IDLE;
                }
                if (isCharging)
                {
                    progressState = InteractProgressState.INCREASING;
                }
                else
                {
                    progressBar.value -= PROGRESS_RATE * Time.deltaTime;
                }
                break;

            case InteractProgressState.FULL:
                OnInteractionComplete();
                PlayOnCompleteSound();
                
                progressState = InteractProgressState.IDLE;
                isCharging = false;
                break;
        }
    }
    public void RemoveChargeListener()
    {
        actions.OnInteractHeld_Started.RemoveListener(Charge);
    }
    public void AddChargeListener()
    {
        actions.OnInteractHeld_Started.AddListener(Charge);
    }
    public void Charge()
    {
        isCharging = true;
    }
    public void UnCharge()
    {
        isCharging = false;
    }
    public void HighlightSprite()
    {
        sr.sprite = workstationSO.highlightedSprite;
        progBarCanvas.SetActive(true);
    }
    public void NormalSprite()
    {
        sr.sprite = workstationSO.normalSprite;
        progBarCanvas.SetActive(false);
    }
    private void PlayOnCompleteSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(workstationOnCompleteSound, transform.position);
    }

    private void PlayOnFailSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(workstationFailed, transform.position);
    }
}

public enum InteractProgressState
{
    IDLE,
    INCREASING,
    DECREASING,
    FULL
}
