using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipePanel : MonoBehaviour
{
    public GameObject selectorPrefab;

    private GameObject selectorContent;
    private GameObject recipeInfoPanel;
    private Button addItemsButton;

    private InventoryManagerV2 playerInventory;

    
    // Start is called before the first frame update
    void Start()
    {
        var machineUI = GameObject.Find("MachineInventoryContainer");
        machineUI.SetActive(true);
        selectorContent = GameObject.Find("RecipePanel/RecipeSelectPanel/RecipeSelectorView/Viewport/Content");
        addItemsButton = GameObject.Find("RecipePanel/RecipeInfoPanel/AddItemsButton").GetComponent<Button>();
        recipeInfoPanel = GameObject.Find("RecipePanel/RecipeInfoPanel");
        playerInventory = GameObject.Find("PlayerInventoryContainer").GetComponent<InventoryManagerV2>();

        addItemsButton.onClick.AddListener(()=>GameEvents.current.MakeRecipeClick(recipeInfoPanel.GetComponent<RecipeMetadata>().recipe));

        GameEvents.current.onInventoryChange += OnInventoryChanged;
        recipeInfoPanel.SetActive(false);
        machineUI.SetActive(false);
    }


    private void ClearRecipes()
    {
        int totalChildren = selectorContent.transform.childCount;
        for (int i = 0; i < totalChildren; i++)
        {
            Destroy(selectorContent.transform.GetChild(i).gameObject);
        }
    }

    public void AddRecipes(List<CraftingRecipe> validRecipes)
    {
        ClearRecipes();

        foreach (var recipe in validRecipes)
        {
            var selectorButton = Instantiate(selectorPrefab, selectorContent.transform, false);
            selectorButton.GetComponent<Button>().onClick.AddListener(() => {
                recipeInfoPanel.GetComponent<RecipeMetadata>().OnSelectRecipe(recipe);
                addItemsButton.enabled = playerInventory.HasAllItems(recipe.GetInputDictionary());
            });
            selectorButton.GetComponentInChildren<Text>().text = recipe.recipeName;
        }
    }

    private void OnInventoryChanged(int id)
    {
        if (id == playerInventory.id)
        {
            CraftingRecipe recipe = recipeInfoPanel.GetComponent<RecipeMetadata>().recipe;
            if (recipe)
            {
                addItemsButton.enabled = playerInventory.HasAllItems(recipe.GetInputDictionary());
            }
        }
    }

    private void OnDestroy()
    {
        GameEvents.current.onInventoryChange -= OnInventoryChanged;
    }
}
