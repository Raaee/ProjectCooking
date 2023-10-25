using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class InteractionDetector : MonoBehaviour {

    public List<Interactable> interactablesInRange = new List<Interactable>();
    public List<IWorkstation> workStationsInRange = new List<IWorkstation>();
    private Actions actions;

    private void Awake() {
        actions = GetComponent<Actions>();
        actions.OnInteract.AddListener(Interacted);
        actions.OnInteractHeld_Started.AddListener(Worked);
    }
    public void Interacted() {
        if (interactablesInRange.Count > 0) {
            var interactable = interactablesInRange[0];
            interactable.Interact();
        }
    }
    public void Worked() {
        if (workStationsInRange.Count > 0) {
            var workstation = workStationsInRange[0];
            workstation.Interact();
            Debug.Log("workstation");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var interactable = collision.GetComponent<Interactable>();
        var workstation = collision.GetComponent<IWorkstation>();

        if (interactable != null) {
            interactablesInRange.Add(interactable);
        }
        if (workstation != null) {
            workStationsInRange.Add(workstation);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        var interactable = collision.GetComponent<Interactable>();
        var workstation = collision.GetComponent<IWorkstation>();

        if (interactablesInRange.Contains(interactable)) {
            interactablesInRange.Remove(interactable);
        }
        if (workStationsInRange.Contains(workstation)) {
            workStationsInRange.Remove(workstation);
        }
    }
}
