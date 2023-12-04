using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe")]
public class RecipeSO : ScriptableObject
{
    public List<RecipeStep> recipeSteps;
}
