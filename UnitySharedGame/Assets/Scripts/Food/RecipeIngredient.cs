using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeIngredient
{
    private Item item;
    private int quantity;

    public RecipeIngredient(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public override string ToString()
    {
        return string.Format("[Ingredient: {0}-{1}]", item, quantity);
    }
}
