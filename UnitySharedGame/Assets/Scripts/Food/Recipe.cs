using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : BaseGameObject
{
    private List<RecipeIngredient> recipeIngredients;

    private Recipe()
    {
        recipeIngredients = new List<RecipeIngredient>();
    }

    public override string ToString()
    {
        return recipeIngredients.ToString();
    }
}
