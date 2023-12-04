using System.Collections.Generic;
using UnityEngine;

public class CookbookRecipeDisplay : MonoBehaviour
{
    [SerializeField] private RecipeSO recipeSO;
    public List<RecipeStepUIData> recipeStepUIDataList;
    public Sprite chopSprite;
    public Sprite mixSprite;
    public Sprite stovenSprite;
    private void Start()
    {

        //only show the correct amount of recipeStepUIDatas, change the i in the bottom iterator 
        int amtOfRecipeSteps = recipeSO.recipeSteps.Count;

        //we are deleting the last (5 - amtOfRecipeSteps) panels
        int panelsToHide = 5 - amtOfRecipeSteps;
        for (int i = 4; i > panelsToHide; i--)
        {
            recipeStepUIDataList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < amtOfRecipeSteps; i++)
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
        for (int i = 0; i < 3; i++)
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
