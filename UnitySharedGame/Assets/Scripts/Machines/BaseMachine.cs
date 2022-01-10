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

    private GameObject MachineUIContainer;
    
    public InventoryManagerV2 playerInventory;
    public RecipePanel recipePanel; //maybe make static

   

    private ProgressBar progressBar;
    private Button completeCookButton;

    private CraftingRecipe currentRecipe;

    private bool playerNearby;
    private bool activeState;



    // Start is called before the first frame update
    void Start()
    {
        MachineUIContainer = GameObject.Find("MachineInventoryContainer");
        machineInventoryUIManager = GameObject.Find("MachineUI").GetComponent<MachineInventoryUIManager>();

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
            if(machineInventoryUIManager.outputSlotManager.AddAllItems(output, considerOutputSlots: true)){
                machineInventoryUIManager.inputSlotManager.ForceClearSlots();
                // reset stuff
                currentRecipe = null;
                UpdateCookButton();
            }
        });

        UpdateCookButton();
        //internalUI.SetActive(false);
        //UIParentContainer.SetActive(false);
        GameEvents.current.onInventorySlotClick += OnInventorySlotClicked;
        GameEvents.current.onMakeRecipeClick += MoveIngredientsFromPlayerToMachine;
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

    private void OpenCloseUI()
    {
        activeState = !activeState;
        MachineUIContainer.SetActive(activeState);

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
