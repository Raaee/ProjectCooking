using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// base abstract class for abilities 
/// </summary>
public abstract class Ability : MonoBehaviour
{
    protected const float QUICK_COOLDOWN = 1.0f; 
    private float timer = 0f;
    //TODO: add a range attribute for game design changes
    [SerializeField] protected float abilityDuration;
    [SerializeField] protected int bloodCost;
    protected bool isPerformingAbility = false;
    protected Actions actions;

    [Header("ABILITY GLOBAL REFERENCES")] 
    [SerializeField] protected ProgressBar bloodProgressBar;
    [Header("AUDIO")]
    [SerializeField] private FMODUnity.EventReference abilityAudioRef;
    public virtual void Awake() {
        
        if (!bloodProgressBar)
        {
            Debug.LogWarning("(cant find progress bar component, please assign in inspector)");
            bloodProgressBar = FindAnyObjectByType<ProgressBar>();
        }
         
        actions = GetComponent<Actions>();
    }
    private void Update() {
        if (isPerformingAbility) {
            timer += Time.deltaTime;
        }

        if (timer >= abilityDuration) {
            timer = 0f;
            isPerformingAbility = false;
            OnAbilityEnd();
        }
    }
    public void PerformAbility() {
       
        //check if amount avaible 
        if (isPerformingAbility == true) {
            OnCantPerform();
            return;
        }

        if (bloodProgressBar.GetCurrentBarAmt() < bloodCost) {
            OnNotEnoughBlood();
            return;
        }

        bloodProgressBar.Decrease(bloodCost);

        //movement and attack speed increases for a set amount of time 
        isPerformingAbility = true;
        OnAbilityStart();
        PlayAbilityOneShot();
    }

    protected void PlayAbilityOneShot()
    {
        if(!abilityAudioRef.IsNull)
            FMODUnity.RuntimeManager.PlayOneShot(abilityAudioRef, transform.position);
    }

    public abstract void OnCantPerform();
    public abstract void OnNotEnoughBlood();
    public abstract void OnAbilityStart();
    public abstract void OnAbilityEnd();

   
   
}
