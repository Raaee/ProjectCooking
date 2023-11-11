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
    private const int ZERO = 0; //mind your business raeus, i wanted to see how it looks 

    private void Awake() {
        actions = GetComponent<Actions>();
        actions.OnInteract.AddListener(OnInteract_InteractionDetect);
    }   
    private void OnInteract_InteractionDetect() {

        if (interactablesInRange.Count > ZERO) {
            IInteractable interactable = interactablesInRange[ZERO];
            interactable.Interact();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        Workstation workstation = collision.GetComponent<Workstation>();

        if (interactable != null) {
            interactablesInRange.Add(interactable);
            interactable.HighlightSprite();
        }
        if (workstation != null) {
            workStationsInRange.Add(workstation);
            workstation.AddChargeListener();
            workstation.HighlightSprite();
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        Workstation workstation = collision.GetComponent<Workstation>();

        if (interactablesInRange.Contains(interactable)) {
            interactablesInRange.Remove(interactable);
            interactable.NormalSprite();
        }
        if (workStationsInRange.Contains(workstation)) {
            workStationsInRange.Remove(workstation);
            workstation.RemoveChargeListener();
            workstation.NormalSprite();
        }
    }
}
