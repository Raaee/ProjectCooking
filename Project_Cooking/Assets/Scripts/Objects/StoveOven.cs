using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StoveOven : Interactable, IWorkstation {

    [SerializeField] protected float interactProgress = 0f;
    [SerializeField] protected Slider progressBar;
    protected bool isCharging;

    public  void Interact() {
       // actions.OnInteractHeld_Started.AddListener(Charge);
       // actions.OnInteractHeld_Started.AddListener(StartProgressCharge);
       // actions.OnInteractHeld_Cancelled.AddListener(UnCharge);
    }
    public void StartProgressCharge() {
       // StartCoroutine(ChargeBarRoutine());
    }
    public void Charge() {
        isCharging = true;
    }
    public void UnCharge() {
        isCharging = false;
    }
    public IEnumerator ChargeBarRoutine() {
        Debug.LogWarning(progressBar.value);
        while (isCharging) {
            progressBar.value += 1.0f * Time.deltaTime;
            yield return null;
        }
        while (!isCharging) {
            progressBar.value -= 1.0f * Time.deltaTime;
            yield return null;
        }
        // this keeps filling exponentially. i need it to be linear but brain cannot brain.
    }    
    // interact held started > increment progress charge
    // if interact cancelled and progress charge != full, decrement charge
    // else  if interact performed, charge = full
}
