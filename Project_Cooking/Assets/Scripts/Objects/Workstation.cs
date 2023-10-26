using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public abstract class Workstation : MonoBehaviour {

    [SerializeField] protected Slider progressBar;
    private Actions actions;

    [Header("DEBUG")]
    [SerializeField] protected InteractProgressState progressState;
    [SerializeField] protected bool isCharging;

    private void Awake() {
        progressState = InteractProgressState.IDLE;
        actions = FindObjectOfType<Actions>();
        actions.OnInteractHeld_Cancelled.AddListener(UnCharge);
    }
    public void RemoveListener() {
        actions.OnInteractHeld_Started.RemoveListener(Charge);
    }
    public void AddListener() {
        actions.OnInteractHeld_Started.AddListener(Charge);
    }
    public void Charge() {
        isCharging = true;
    }
    public void UnCharge() {
        isCharging = false;
    }
    private void Update() {
        // Progress Bar State Machine:
        switch (progressState) {
            case InteractProgressState.IDLE:
                progressBar.value = progressBar.minValue;
                if (isCharging) {
                    progressState = InteractProgressState.INCREASING;
                }
                break;
            case InteractProgressState.INCREASING:
                if (progressBar.value >= progressBar.maxValue) {
                    progressState = InteractProgressState.FULL;
                } 
                if (!isCharging) {
                    progressState = InteractProgressState.DECREASING;
                } else {
                    progressBar.value += 1.0f * Time.deltaTime;
                }
                break;
            case InteractProgressState.DECREASING:
                if (progressBar.value <= progressBar.minValue) {
                    progressState = InteractProgressState.IDLE;
                }
                if (isCharging) {
                    progressState = InteractProgressState.INCREASING;
                } else {
                    progressBar.value -= 1.0f * Time.deltaTime;
                }
                break;
            case InteractProgressState.FULL:
                OnInteractionComplete();
                progressState = InteractProgressState.IDLE;
                isCharging = false;
                break;
        }
    }
    public abstract void OnInteractionComplete();
}
public enum InteractProgressState { 
    IDLE,
    INCREASING,
    DECREASING,
    FULL
}

