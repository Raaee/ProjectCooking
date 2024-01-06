using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AllRecipeData : MonoBehaviour {

    public static AllRecipeData instance;
    public List<RecipeSO> allRecipes = new List<RecipeSO>();
    public RecipeSO levelRecipe;
    private int currLvlIndex = 0;

    void Awake() {
        Init();
    }
    private void Start() {
        ShuffleRecipes();
    }
    private void Init() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
        
        DontDestroyOnLoad(this.gameObject);
    }
    public void ShuffleRecipes() {
        //get the list of all the recipes and randomly switch their positions, each level we go through each one

        currLvlIndex = 0;

        for (int i = allRecipes.Count - 1; i > 0; i--) {
            int j = Random.Range(0, i + 1);
            RecipeSO temp = allRecipes[i];
            allRecipes[i] = allRecipes[j];
            allRecipes[j] = temp;
        }
        
    }
    public void GetRecipe() {
        levelRecipe = allRecipes[currLvlIndex];
    }
    public void NextLevel() {
        currLvlIndex++;
    }
    public int GetCurrLvlIndex() {
        return currLvlIndex;
    }
}
