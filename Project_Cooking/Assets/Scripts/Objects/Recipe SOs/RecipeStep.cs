using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeStep
{
    public RecipeAction recipeAction;
    [Range(1, 3)]public int amountOfChopAction = 1;
    public List<IngredientSO> recipeIngredients;

}
