using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public abstract class Workstation : MonoBehaviour {

    [SerializeField] protected Slider progressBar;
    private Actions actions;

    [Header("DEBUG")]
    [SerializeField] protected InteractProgressState progressState;
    [SerializeField] protected bool isCharging;
    private const float  PROGRESS_RATE = 1.0f;

    public abstract void OnInteractionComplete();

    private void Awake() {
        progressState = InteractProgressState.IDLE;
        actions = FindObjectOfType<Actions>();
        actions.OnInteractHeld_Cancelled.AddListener(UnCharge);
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
    private void Update() {
        ProgressBarStateMachine();
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
                progressState = InteractProgressState.IDLE;
                isCharging = false;
                break;
        }
    }

  
}

public enum InteractProgressState { 
    IDLE,
    INCREASING,
    DECREASING,
    FULL
}

/* Pete cool and amazing coding logic 
 * the 2 input actions we want are isKeyPressed, and the key released
 * We only want to check/start isKeyPressed only when the player is interacting/vincinity of the workstation
 * 
 * already implemented is a working state machine 
 */
