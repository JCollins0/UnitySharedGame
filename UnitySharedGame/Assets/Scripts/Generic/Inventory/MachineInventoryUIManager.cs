using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineInventoryUIManager : MonoBehaviour
{
    public GameObject inputSlotsParent;
    public GameObject outputSlotsParent;
    public ProgressBar progressBar;

    public GameObject slotUIPrefab;

    public InventoryManagerV2 inputSlotManager;
    public InventoryManagerV2 outputSlotManager;

    private void Start()
    {
    }

    private void ClearSlots()
    {
        inputSlotManager.ClearSlots();
        outputSlotManager.ClearSlots();
    }

    public void SetSlots(List<InventorySlot> slots)
    {
        ClearSlots();
        for (int i = 0; i < slots.Count; i++)
        {
            // using i inside the lamda function uses latest closure value of which would be 
            // slots.Count + 1 causing OOB exception
            int j = i;
            RectTransform transform = (slots[j].slotType == SlotType.INPUT) ? (RectTransform)inputSlotsParent.transform : (RectTransform)outputSlotsParent.transform;
            var slotObj = Instantiate(slotUIPrefab, transform.position, Quaternion.identity, transform);
            slotObj.GetComponent<InventorySlotUI>().slot = slots[j];
            slotObj.GetComponent<Button>().onClick.AddListener(
                () => {
                    Debug.LogFormat("Testing sending an event click: {0}", j);
                    GameEvents.current.InventorySlotClick(slotObj.GetComponent<InventorySlotUI>().slot);
                }
            );
            switch (slots[j].slotType)
            {
                case SlotType.INPUT: inputSlotManager.AddSlot(slotObj);
                    break;
                case SlotType.OUTPUT: outputSlotManager.AddSlot(slotObj);
                    break;
            }

        }
    }

    public void SetProgressBarRecipe(CraftingRecipe recipe)
    {
        progressBar.LoadRecipe(recipe);
    }

    public void UpdateProgressBar(int value)
    {
        progressBar.currentValue = value;
    }

}
