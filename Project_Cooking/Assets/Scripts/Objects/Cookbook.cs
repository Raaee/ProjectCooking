using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookbook : MonoBehaviour, IInteractable {

    public List<WorkstationRecipe> allRecipes = new List<WorkstationRecipe>();
    public List<WorkstationRecipe> recipesAlreadyCompleted = new List<WorkstationRecipe>();
    public WorkstationRecipe levelRecipe;

    private void Awake() {
        PickRandomRecipe();
    }

    public void PickRandomRecipe() {
       // var ran = new Random();
        int ranNum;

        do {
           ranNum = Random.Range(0, allRecipes.Count);

        } while (!recipesAlreadyCompleted.Contains(allRecipes[ranNum]));
        
        levelRecipe = allRecipes[ranNum];
        recipesAlreadyCompleted.Add(levelRecipe);
    }

    public void Interact() {
        Debug.Log("Cookbook interact");
    }
    
    public void HighlightSprite() {
    }
    public void NormalSprite() {
    }
}
