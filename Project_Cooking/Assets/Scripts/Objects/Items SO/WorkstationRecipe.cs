using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Workstation Recipe")]
public class WorkstationRecipe : ScriptableObject {

    public List<IngredientSO> workstationInput;
    public IngredientSO workstationOutput;
    public List<IngredientSO> recipeSteps;
}
