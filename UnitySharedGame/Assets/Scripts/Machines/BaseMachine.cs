using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BaseMachine : BaseGameObject
{
    public List<CraftingRecipe> validRecipes = new List<CraftingRecipe>();
    public GameObject staticUiPrefab;
    public GameObject UIParentContainer;
    public GameObject selectorPrefab;
    public GameObject inventoryPrefab;
    public InventoryManager playerInventory;

    private GameObject recipeInfoPanel;
    private GameObject selectorContent;
    private GameObject inventoryContainer;
    private Button addItemsButton;
    private InventoryManager inputSlotManager;
    private InventoryManager outputSlotManager;

    public void CollectOutput(Button button)
    {

    }

    public void AddRecipe(CraftingRecipe recipe)
    {
        validRecipes.Add(recipe);
    }

    // Start is called before the first frame update
    void Start()
    {
        var internalUI = Instantiate(staticUiPrefab, UIParentContainer.transform, false);
        
        recipeInfoPanel = GameObject.Find("RecipeInfoPanel");
        

        addItemsButton = GameObject.Find("RecipeInfoPanel/AddItemsButton").GetComponent<Button>();

        addItemsButton.onClick = MoveIngredientsFromPlayerToMachine();

        selectorContent = GameObject.Find("RecipeSelectorView/Viewport/Content");

        inventoryContainer = GameObject.Find("InventorySection");
        Instantiate(inventoryPrefab, inventoryContainer.transform, false);

        inputSlotManager = GameObject.Find("InputSlots").GetComponent<InventoryManager>();
        outputSlotManager = GameObject.Find("OutputSlots").GetComponent<InventoryManager>();

        InitRecipeSelectorPrefabs();

        recipeInfoPanel.SetActive(false);

        GameEvents.current.onInventoryChange += OnInventoryChanged;
        GameEvents.current.onInventorySlotClick += OnInventorySlotClicked;
    }

    private void OnInventorySlotClicked(InventorySlot obj)
    {
        Debug.LogFormat("{0}",obj.gameObject.name);
    }

    public void InitRecipeSelectorPrefabs()
    {
        foreach(var recipe in validRecipes)
        {
            var selectorButton = Instantiate(selectorPrefab, selectorContent.transform, false);
            selectorButton.GetComponent<Button>().onClick = LoadRecipe(recipe);
        }
    }

    private Button.ButtonClickedEvent LoadRecipe(CraftingRecipe recipe)
    {
        var b = new Button.ButtonClickedEvent();
        b.AddListener(()=>{
            recipeInfoPanel.GetComponent<RecipeMetadata>().OnSelectRecipe(recipe);
            addItemsButton.enabled = playerInventory.HasAllItems(recipe.GetInputDictionary());
        });
        return b;
    }

    private Button.ButtonClickedEvent MoveIngredientsFromPlayerToMachine()
    {
        
        var b = new Button.ButtonClickedEvent();
        b.AddListener(() => {
            CraftingRecipe recipe = recipeInfoPanel.GetComponent<RecipeMetadata>().recipe;
            Dictionary<Item, int> inputItems = recipe.GetInputDictionary();
            if (playerInventory.HasAllItems(inputItems))
            {
                
                if (inputSlotManager.SimulateAddItems(inputItems))
                {
                    Debug.LogFormat("Making Recipe {0}", recipe);
                    foreach (var item in inputItems.Keys)
                    {
                        for (int i = 0; i < inputItems[item]; i++)
                        {
                            inputSlotManager.AddItem(playerInventory.RemoveSingleItem(item));
                        }
                    }

                    //Dispatch Craft
                }
                
               
            }
        });
        return b;
    }

    private void OnInventoryChanged(int id)
    {
        if(id == playerInventory.id)
        {
            CraftingRecipe recipe = recipeInfoPanel.GetComponent<RecipeMetadata>().recipe;
            if (recipe)
            {
                addItemsButton.enabled = playerInventory.HasAllItems(recipe.GetInputDictionary());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameEvents.current.onInventoryChange -= OnInventoryChanged;
        GameEvents.current.onInventorySlotClick -= OnInventorySlotClicked;
    }

}
