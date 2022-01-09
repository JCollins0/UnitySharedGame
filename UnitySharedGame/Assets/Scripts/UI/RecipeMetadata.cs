using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeMetadata : MonoBehaviour
{
    public Text recipeNameLabel;
    public Text recipeIngredientsLabel;
    public CraftingRecipe recipe;

    public void OnSelectRecipe(CraftingRecipe recipe)
    {
        this.recipe = recipe;
        recipeNameLabel.text = recipe.recipeName;
        recipeIngredientsLabel.text = recipe.GetIngredientsText();
        this.gameObject.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
