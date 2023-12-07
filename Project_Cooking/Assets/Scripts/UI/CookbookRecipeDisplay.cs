using System.Collections.Generic;
using UnityEngine;

public class CookbookRecipeDisplay : MonoBehaviour
{
    [SerializeField] private RecipeSO recipeSO;
    public List<RecipeStepUIData> recipeStepUIDataList;
    public Sprite chopSprite;
    public Sprite mixSprite;
    public Sprite stovenSprite;
    private bool cookbookIsOpened = false;


    private void Start()
    {
        CloseCookbook();
    }

    public void OpenCookbook(int playerUnlocked)
    {
        DisplayCurrentRecipeSteps(playerUnlocked);
        cookbookIsOpened = true;
    }

    public void CloseCookbook()
    {
        CloseAllPanels();
        cookbookIsOpened = false;
    }
    private void DisplayCurrentRecipeSteps(int playerUnlocked)//0 is an option
    {
        //if 0, show nothing
        CloseAllPanels();
        if (playerUnlocked == 0)
            return;
        int maxRecipeSteps = recipeSO.recipeSteps.Count;
        if (playerUnlocked > maxRecipeSteps)
            playerUnlocked = maxRecipeSteps;
      
        for (int i = 0; i < playerUnlocked; i++)
        {
            DisplayRecipeStep(recipeSO.recipeSteps[i], recipeStepUIDataList[i]);
            recipeStepUIDataList[i].gameObject.SetActive(true);
        }
    }

    public void DisplayRecipeStep(RecipeStep recipeStep, RecipeStepUIData recipeStepUIData)
    {
        //turn on the specific recipe step panel 
        recipeStepUIData.gameObject.SetActive(true);

        // assign the action image as the workstation
        recipeStepUIData.actionImage.sprite = AssignSpriteFromRecipeAction(recipeStep.recipeAction);

        //iterate through the recipe step ingredients and assign it to the images in recipestepuidatalist
        int amtOfRecipes = recipeStep.recipeIngredients.Count;
        for (int i = 0; i < 3; i++)//at most 3 ingredients
        {
            if (i >= amtOfRecipes)
            {
                recipeStepUIData.recipeIngredientsImages[i].sprite = null;
            }
            else
            {
                recipeStepUIData.recipeIngredientsImages[i].sprite = recipeStep.recipeIngredients[i].normalSprite;
            }
        }
    }
    private void CloseAllPanels()
    {
        foreach (var i in recipeStepUIDataList)
        {
            i.gameObject.SetActive(false);
        }
    }

    private Sprite AssignSpriteFromRecipeAction(RecipeAction action)
    {
        switch (action)
        {
            case RecipeAction.MIX:
                return mixSprite;
            case RecipeAction.CHOP:
                return chopSprite;
            case RecipeAction.BAKE:
                return stovenSprite;
            default:
                Debug.LogError("INVALID RECIPE ACTION");
                break;
        }
        return null;
    }
    public void SetRecipeSO(RecipeSO newRecipe) {
        recipeSO = newRecipe;
    }

    public bool IsCookbookOpen()
    {
        return cookbookIsOpened;
    }
}
