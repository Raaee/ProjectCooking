using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeProgress : MonoBehaviour {

    [SerializeField] private int maxProgress = 100;
    [SerializeField] private int currentProgress = 0;

    public void AddProgress(int amt) {
        currentProgress += amt;
    }
    public void ResetProgress() {
        currentProgress = 0;
    }
    public int GetCurrentProgress() {
        return currentProgress;
    }
    
}
