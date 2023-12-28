using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cookbook : MonoBehaviour, IInteractable
{
    [SerializeField] private CookbookRecipeDisplay recipeDisplay;
    public List<RecipeSO> allRecipes = new List<RecipeSO>();
    public RecipeSO levelRecipe;
    public int nodesUnlocked = 0;

    [Header("VISUAL")]
    [SerializeField] private Sprite cookbookSprite;
    [SerializeField] private Sprite highlightedCookbookSprite;
    private SpriteRenderer sr;
    [HideInInspector] public UnityEvent<float> OnNodeIncreased;
    [Header("DEBUG RECIPE: add a recipe to override the random recipe, else leave null")]
    public RecipeSO debugLevelRecipe;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        PickRandomRecipe();
        recipeDisplay.SetRecipeSO(levelRecipe);
    }

    public void PickRandomRecipe() {
        // refactor: get the list of all the recipes and randomly switch their positions, each level we go through each one

        if (debugLevelRecipe == null)
        {
            int ranNum;
            ranNum = Random.Range(0, allRecipes.Count);

            levelRecipe = allRecipes[ranNum];
            allRecipes.Remove(levelRecipe);
        }
        else
        {
            levelRecipe = debugLevelRecipe;
            allRecipes.Remove(levelRecipe);
        }
       
    }

    

    public void Interact()  {


        //recipeDisplay.DisplayAllRecipeSteps(nodesUnlocked);
        if (!recipeDisplay.IsCookbookOpen())
            recipeDisplay.OpenCookbook(nodesUnlocked);
        else
            recipeDisplay.CloseCookbook(true);
        /*
         * - cookbook determines the level's recipe
         * which is passed into the display
         * - when node is lit in progressBar, cookbook receives next recipe step (display number increases)
         * - cookbook calls display from interact()
         * 
         */
    }

    public void HighlightSprite()
    {
        sr.sprite = highlightedCookbookSprite;
    }
    public void NormalSprite()
    {
        sr.sprite = cookbookSprite;
        if (recipeDisplay.IsCookbookOpen())
            recipeDisplay.CloseCookbook(true);
    }
    public void IncrementNodesUnlocked() {
        nodesUnlocked += 1;
        GetPercentageOfRecipeUnlocked();
    }

    private void GetPercentageOfRecipeUnlocked()
    {
        var numOfSteps = levelRecipe.recipeSteps.Count;
        float percentage = nodesUnlocked / numOfSteps;
        OnNodeIncreased?.Invoke(percentage);
    }
}
