using System.Collections.Generic;
using UnityEngine;

public class CookbookRecipeDisplay : MonoBehaviour
{
    [SerializeField] private RecipeSO recipeSO;
    public List<RecipeStepUIData> recipeStepUIDataList;
    public Sprite chopSprite;
    public Sprite mixSprite;
    public Sprite stovenSprite;
    private const int MAX_AMT_PANELS = 5;
    private void Start()
    {
        //DisplayAllRecipeSteps(4);
    }

    public void DisplayAllRecipeSteps(int playerUnlocked)//how many steps/nodes the player unlocked, should start at 1 not 0
    {
        
        //only show the correct amount of recipeStepUIDatas, change the i in the bottom iterator 
        int maxAmtOfRecipeSteps = recipeSO.recipeSteps.Count;
        if (playerUnlocked > maxAmtOfRecipeSteps)
            playerUnlocked = maxAmtOfRecipeSteps;

        //we are hiding the last x panels
        int panelsToHide = MAX_AMT_PANELS - playerUnlocked;
        int tempPanelCOunt = 1;
        for (int i = 4; i >= 0; i--)
        {
            recipeStepUIDataList[i].gameObject.SetActive(false);
            tempPanelCOunt++;
            if (tempPanelCOunt > panelsToHide)
                break;
        }

        for (int i = 0; i < maxAmtOfRecipeSteps; i++)
        {
            DisplayRecipeStep(recipeSO.recipeSteps[i], recipeStepUIDataList[i]);
        }
    }

    public void DisplayRecipeStep(RecipeStep recipeStep, RecipeStepUIData recipeStepUIData)
    {
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
}
