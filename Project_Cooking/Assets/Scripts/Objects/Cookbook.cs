using System.Collections.Generic;
using UnityEngine;

public class Cookbook : MonoBehaviour, IInteractable
{

    public List<WorkstationRecipe> allRecipes = new List<WorkstationRecipe>();
    public WorkstationRecipe levelRecipe;

    [Header("VISUAL")]
    [SerializeField] private Sprite cookbookSprite;
    [SerializeField] private Sprite highlightedCookbookSprite;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        PickRandomRecipe();
    }

    public void PickRandomRecipe()
    {
        int ranNum;
        ranNum = Random.Range(0, allRecipes.Count);

        levelRecipe = allRecipes[ranNum];
        allRecipes.Remove(levelRecipe);
    }

    public void Interact()
    {
        Debug.Log("Cookbook interact");
    }

    public void HighlightSprite()
    {
        sr.sprite = highlightedCookbookSprite;
    }
    public void NormalSprite()
    {
        sr.sprite = cookbookSprite;
    }
}
