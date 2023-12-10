using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create SO for all ingredients")]
public class _AllIngredientsSO : ScriptableObject
{
    public List<IngredientSO> ingredientSos;
}
