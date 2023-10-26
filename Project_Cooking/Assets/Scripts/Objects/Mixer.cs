using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Mixer {

    [SerializeField] protected float interactProgress = 0f;
    [SerializeField] protected Slider progressBar;

    public void Interact() {
        Debug.Log("Mixer is done working.");
    }
    public void OnInteractionComplete() {

    }
}
