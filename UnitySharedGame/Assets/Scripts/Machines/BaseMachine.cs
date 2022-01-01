using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseMachine : MonoBehaviour
{

    public List<MachineCraftingRecipe> validRecipes = new List<MachineCraftingRecipe>();

    public void AddRecipe(MachineCraftingRecipe recipe)
    {
        validRecipes.Add(recipe);
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
