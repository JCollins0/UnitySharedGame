using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BaseMachine : BaseGameObject
{
    public List<CraftingRecipe> validRecipes = new List<CraftingRecipe>();

    // Slots are defined by scriptable objects in editor
    public List<InventorySlot> slots;
    private MachineInventoryUIManager machineInventoryUIManager;

    //private GameObject MachineUIContainer;
    
    public InventoryManagerV2 playerInventory;
    public RecipePanel recipePanel; //maybe make static

   

    //private ProgressBar progressBar;
    private Button completeCookButton;

    private CraftingRecipe currentRecipe;

    private bool playerNearby;
    private bool activeState;

    private bool isCrafting;

    public int currentCookProgress;

    // Start is called before the first frame update
    void Start()
    {
        MachineInventoryUIContainer.ActivateMachineInventoryUI();
        machineInventoryUIManager = GameObject.Find("MachineUI").GetComponent<MachineInventoryUIManager>();

        completeCookButton = GameObject.Find("CompleteCookButton").GetComponent<Button>();
        completeCookButton.onClick.AddListener(CompleteCraft);

        UpdateCookButton();
        //internalUI.SetActive(false);
        //UIParentContainer.SetActive(false);
        GameEvents.current.onInventorySlotClick += OnInventorySlotClicked;
        GameEvents.current.onMakeRecipeClick += MoveIngredientsFromPlayerToMachine;
        MachineInventoryUIContainer.DeActivateMachineInventoryUI();
    }

    private void CompleteCraft()
    {
        // get current recipe,
        CraftingRecipe recipe = this.currentRecipe;
        // get output based on progress time

        Dictionary<Item, int> output = recipe.GetRecipeOutputBasedOnCookTime(currentCookProgress);
        // remove input and move to output
        if (machineInventoryUIManager.outputSlotManager.AddAllItems(output, considerOutputSlots: true))
        {
            machineInventoryUIManager.inputSlotManager.ForceClearSlots();
            // reset stuff
            ResetCraft();
            UpdateCookButton();
        }
    }

    private void ResetCraft()
    {
        currentRecipe = null;
        isCrafting = false;
        currentCookProgress = 0;
    }

    private void OnInventorySlotClicked(InventorySlot slot)
    {
        Debug.Log("Listening For Event");
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
            if (machineInventoryUIManager.inputSlotManager.AddAllItems(inputItems))
            {

                playerInventory.RemoveMultipleItems(inputItems);
                Debug.LogFormat("Making Recipe {0}", recipe);

                //Dispatch Craft
                machineInventoryUIManager.SetProgressBarRecipe(recipe);
                this.currentRecipe = recipe;
                isCrafting = true;
                UpdateCookButton();
            }
        }
    }


    private void UpdateCookButton()
    {
        completeCookButton.enabled = this.currentRecipe != null;
    }

    private void OpenCloseUI()
    {
        activeState = !activeState;
        MachineInventoryUIContainer.UpdateState(activeState);

        if (activeState)
        {
            recipePanel.AddRecipes(validRecipes);
            machineInventoryUIManager.SetSlots(slots);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerNearby)
        {
            OpenCloseUI();
        }
    }

    private void FixedUpdate()
    {
        if (isCrafting)
        {
            if (currentCookProgress < 100)
            {
                currentCookProgress++;
            }

            if(currentCookProgress == 100)
            {
                CompleteCraft();
            }
        }

        if (activeState)
        {
            machineInventoryUIManager.UpdateProgressBar(currentCookProgress);
        }

    }

    private void OnDestroy()
    {
        GameEvents.current.onMakeRecipeClick -= MoveIngredientsFromPlayerToMachine;
        GameEvents.current.onInventorySlotClick -= OnInventorySlotClicked;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerNearby = false;
        }
    }

}
