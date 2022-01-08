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
    
    public GameObject inventoryPrefab;
    public InventoryManagerV2 playerInventory;
    public RecipePanel recipePanel;

    private GameObject inventoryContainer;
    private InventoryManagerV2 inputSlotManager;
    private InventoryManagerV2 outputSlotManager;

    private GameObject internalUI;
    private ProgressBar progressBar;
    private Button completeCookButton;

    private CraftingRecipe currentRecipe;

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
        //UIParentContainer.SetActive(true);
        internalUI = Instantiate(staticUiPrefab, UIParentContainer.transform, false);
        
        inventoryContainer = GameObject.Find("InventorySection");
        Instantiate(inventoryPrefab, inventoryContainer.transform, false);

        inputSlotManager = GameObject.Find("InputSlots").GetComponent<InventoryManagerV2>();
        outputSlotManager = GameObject.Find("OutputSlots").GetComponent<InventoryManagerV2>();

        progressBar = GameObject.Find("ProgressBar").GetComponent<ProgressBar>();
        completeCookButton = GameObject.Find("CompleteCookButton").GetComponent<Button>();
        completeCookButton.onClick.AddListener(() =>
        {
            // get current recipe,
            CraftingRecipe recipe = this.currentRecipe;
            // get output based on progress time
            int cookProgress = progressBar.currentValue;
            Dictionary<Item,int> output = recipe.GetRecipeOutputBasedOnCookTime(cookProgress);
            // remove input and move to output
            inputSlotManager.ForceClearSlots();
            outputSlotManager.AddAllItems(output, considerOutputSlots: true);

            // reset stuff
            currentRecipe = null;
            UpdateCookButton();
        });

        UpdateCookButton();

        //internalUI.SetActive(false);
        //UIParentContainer.SetActive(false);
        GameEvents.current.onInventorySlotClick += OnInventorySlotClicked;
        GameEvents.current.onMakeRecipeClick += MoveIngredientsFromPlayerToMachine;
    }

    private void OnInventorySlotClicked(InventorySlot slot)
    {
        Tuple<Item, int> output = slot.PeekOutput();
        Dictionary<Item, int> outputDict = new Dictionary<Item, int>
        {
            { output.Item1, output.Item2 }
        };
        if (playerInventory.AddAllItems(outputDict))
        {
            slot.ForceClearSlot();
        }
    }



    private void MoveIngredientsFromPlayerToMachine(CraftingRecipe recipe)
    {

        Dictionary<Item, int> inputItems = recipe.GetInputDictionary();
        if (playerInventory.HasAllItems(inputItems))
        {

            if (inputSlotManager.AddAllItems(inputItems))
            {

                playerInventory.RemoveMultipleItems(inputItems);
                Debug.LogFormat("Making Recipe {0}", recipe);
                
                //Dispatch Craft
                progressBar.LoadRecipe(recipe);
                this.currentRecipe = recipe;

                UpdateCookButton();
            }


        }
    }


    private void UpdateCookButton()
    {
        completeCookButton.enabled = this.currentRecipe != null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            bool turnOn = !internalUI.activeSelf;
            internalUI.SetActive(!internalUI.activeSelf);
            UIParentContainer.SetActive(!UIParentContainer.activeSelf);

            if (turnOn)
            {
                recipePanel.AddRecipes(validRecipes);
            }
            
        }
    }

    private void OnDestroy()
    {
        GameEvents.current.onMakeRecipeClick -= MoveIngredientsFromPlayerToMachine;
        GameEvents.current.onInventorySlotClick -= OnInventorySlotClicked;
    }

}
