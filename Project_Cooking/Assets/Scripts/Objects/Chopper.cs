using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Chopper {

    [SerializeField] protected float interactProgress = 0f;
    [SerializeField] protected Slider progressBar;

    public void Interact() {
        Debug.Log("Chopper is done working.");
    }
    public void OnInteractionComplete() {

    }
}
