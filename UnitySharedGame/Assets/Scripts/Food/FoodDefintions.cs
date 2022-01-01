

public class FoodDefintions
{
    /*
        TODO: Set buy and sell prices for items
        Items that cannot be bought can have price of 0?
        Items that cannot be bought should have price of 0?

        Cookable items you can set the min and max processing times to determine when the item will be still raw,
        cooked or burnt: ie) 10 -> 20 means [0-9] will be raw [10-20] will be cooked [21+] will be burnt 
    */

    //// Bread
    //public static readonly Item bread = ItemFactory.Create().Name("Bread").BuyPrice(0).SellPrice(0).Build();
    //public static readonly Item slicedBread = ItemFactory.Create().Name("Sliced Bread").BuyPrice(0).SellPrice(0).Build();
    //public static readonly Item toast = ItemFactory.Create().Name("Toast").BuyPrice(0).SellPrice(0).Build();

    //// Meat
    //public static readonly Item chicken = ItemFactory.Create().Name("Raw Chicken").Status(ItemStatus.RAW).Build();
    //public static readonly Item chickenCutlet = ItemFactory.Create().Name("Chicken Cutlet").Status(ItemStatus.RAW).Build();
    //public static readonly Item cookedChickenCutlet = ItemFactory.Create().Name("Cooked Chicken Cutlet").Status(ItemStatus.NOT_SET).Build();

    //// Cutting Board Recipes
    //public static readonly MachineCraftingRecipe slicedBreadRecipe = MachineCraftingRecipeFactory.Create().
    //    Input(bread,1).ProcessingTime(0).Output(slicedBread,8).Build();
    //public static readonly MachineCraftingRecipe chickenCutletRecipe = MachineCraftingRecipeFactory.Create().
    //    Input(chicken, 1).ProcessingTime(0).Output(chickenCutlet, 10).Build();

    //// Toaster Recipes
    //public static readonly MachineCraftingRecipe toastRecipe = MachineCraftingRecipeFactory.Create().
    //    Input(slicedBread, 1).ProcessingTime(0).Output(toast, 1).Build();


    //// Blender Recipes

    //// Oven Recipe
    //public static readonly MachineCraftingRecipe cookedChickenCutletRecipe = MachineCraftingRecipeFactory.Create().
    //    Input(chickenCutlet, 1).MinProcessingTime(0).MaxProcessingTime(0).Output(cookedChickenCutlet, 1).Build();


    //// Normal Crafting Recipes
    //public static readonly Recipe boringChickenSandwhich = RecipeFactory.Create().
    //    AddIngredient(slicedBread,1).AddIngredient(cookedChickenCutlet,1).Build();

    //public FoodDefintions()
    //{
    //    Debug.Log(slicedBreadRecipe);
    //}

}
