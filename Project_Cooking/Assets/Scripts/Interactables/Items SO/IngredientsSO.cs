using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient")]
public class IngredientSO : ScriptableObject
{

    public GameObject prefab;
    public Sprite normalSprite;
    public Sprite highlightedSprite;
    public string displayName;
    public Items item;
}
