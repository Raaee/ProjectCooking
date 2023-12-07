using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        PickRandomRecipe();
        recipeDisplay.SetRecipeSO(levelRecipe);
    }

    public void PickRandomRecipe()
    {
        int ranNum;
        ranNum = Random.Range(0, allRecipes.Count);

        levelRecipe = allRecipes[ranNum];
      //  allRecipes.Remove(levelRecipe);
    }

    public void Interact()  {


        //recipeDisplay.DisplayAllRecipeSteps(nodesUnlocked);
        if (!recipeDisplay.IsCookbookOpen())
            recipeDisplay.OpenCookbook(nodesUnlocked);
        else
            recipeDisplay.CloseCookbook();
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
    }
    public void IncrementNodesUnlocked() {
        Debug.Log("Incremented");
        nodesUnlocked += 1;
    }
}
