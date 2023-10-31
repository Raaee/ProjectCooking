using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class InteractionDetector : MonoBehaviour {

    public List<Workstation> workStationsInRange = new List<Workstation>();
    public List<IInteractable> interactablesInRange = new List<IInteractable>();
    private Actions actions;

    private void Awake() {
        actions = GetComponent<Actions>();
        actions.OnInteract.AddListener(OnInteract);
    }   
    private void OnInteract() {

        if (interactablesInRange.Count > 0) {
            IInteractable interactable = interactablesInRange[0];
            interactable.Interact();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        Workstation workstation = collision.GetComponent<Workstation>();

        if (interactable != null) {
            interactablesInRange.Add(interactable);
        }
        if (workstation != null) {
            workStationsInRange.Add(workstation);
            workstation.AddChargeListener();
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        Workstation workstation = collision.GetComponent<Workstation>();

        if (interactablesInRange.Contains(interactable)) {
            interactablesInRange.Remove(interactable);
        }
        if (workStationsInRange.Contains(workstation)) {
            workStationsInRange.Remove(workstation);
            workstation.RemoveChargeListener();
        }
    }
}
