using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
/// <summary>
///the worst code ever made, will continue to give me PTSD - Peterson N. 10/29/2023
/// </summary>
public class InteractionDetector : MonoBehaviour {

    public List<Workstation> workStationsInRange = new List<Workstation>();
    public List<IInteractable> interactablesInRange = new List<IInteractable>();
    private Actions actions;

    private void Awake() {
        actions = GetComponent<Actions>();
        actions.OnInteract.AddListener(OnInteract_InteractionDetect);
    }   
    private void OnInteract_InteractionDetect() {

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
