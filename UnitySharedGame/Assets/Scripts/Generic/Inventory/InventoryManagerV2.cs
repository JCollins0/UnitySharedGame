using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class InventoryManagerV2 : BaseGameObject
{

    public List<GameObject> slots;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            // using i inside the lamda function uses latest closure value of which would be 
            // slots.Count + 1 causing OOB exception
            int j = i;
            slots[j].transform.SetParent(transform);
            slots[j].GetComponent<Button>().onClick.AddListener( 
                () => GameEvents.current.InventorySlotClick(slots[j].GetComponent<InventorySlot>())
            );
        }
    }


    /*
        Adding Items
    */
    private bool CanAdd(Item item, int quantity = 1, bool considerOutputSlots = false)
    {
        int itemsLeftToAdd = quantity;

        foreach (var slotObj in slots)
        {
            var slot = slotObj.GetComponent<InventorySlot>();
            var result = slot.CanAdd(item, itemsLeftToAdd, considerOutputSlots);
            if (result.Item1)
            {
                itemsLeftToAdd -= result.Item2;
                if (itemsLeftToAdd == 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool CanAddAllItems(Dictionary<Item, int> inputItemQuantities, bool considerOutputSlots = false)
    {
        List<InventorySlotScriptable> transaction = new List<InventorySlotScriptable>();
        slots.ForEach((slotObj) =>
        {
            var slot = slotObj.GetComponent<InventorySlot>();
            if (slot.slotType == SlotType.INPUT || slot.slotType == SlotType.IN_OUT || considerOutputSlots)
            {
                var fakeSlot = new InventorySlotScriptable
                {
                    heldItem = slot.heldItem,
                    quantity = slot.quantity,
                    maxStackSize = slot.maxStackSize
                };
                transaction.Add(fakeSlot);
            }
        });

        foreach (var item in inputItemQuantities.Keys)
        {
            int needToAdd = inputItemQuantities[item];
            // go through each slot
            foreach (var slot in transaction)
            {
                for(int i = 0; i < needToAdd && slot.CanAdd(item, 1, considerOutputSlots).Item1 && slot.SimulateAdd(item); i++)
                {
                    needToAdd--;
                }
            }
            if (needToAdd > 0)
            {
                return false;
            }
        }

        return true;
    }

    private bool AddItem(Item item, bool considerOutputSlots=false,  bool fireEventsOnAdd=true)
    {
        foreach (var slotObj in slots)
        {
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            if (slot.slotType == SlotType.INPUT || slot.slotType == SlotType.IN_OUT || considerOutputSlots)
            {
                if (slot.AttemptAdd(item))
                {
                    if (fireEventsOnAdd)
                    {
                        GameEvents.current.InventoryChange(this.id);
                    }
                    
                    return true;
                }
            }
        }
        return false;

    }

    public bool AddAllItems(Dictionary<Item, int> inputItemQuantities, bool considerOutputSlots=false, bool fireEventsOnAdd = true)
    {
        if(CanAddAllItems(inputItemQuantities, considerOutputSlots))
        {
            foreach (Item item in inputItemQuantities.Keys)
            {
                for(int i = 0; i < inputItemQuantities[item]; i++)
                {
                    AddItem(item, considerOutputSlots, false);
                }
            }
            if (fireEventsOnAdd)
            {
                GameEvents.current.InventoryChange(this.id);
            }
            return true;
        }

        return false;
        
    }

    /*
      Removing items

    */

    private bool HasItem(Item item, int quantityNeeded = 1, bool considerInputSlots = false)
    {

        Dictionary<int, int> amountLookup = new Dictionary<int, int>();
        amountLookup[item.id] = 0;

        foreach (var slotObj in slots)
        {
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            amountLookup[item.id] += slot.HasItem(item, considerInputSlots);
        }

        if (amountLookup[item.id] >= quantityNeeded)
        {
            return true;
        }

        return false;
    }

    public bool HasAllItems(Dictionary<Item, int> inputItemQuantities, bool considerInputSlots = false)
    {
        foreach (var item in inputItemQuantities.Keys)
        {
            if (!HasItem(item, inputItemQuantities[item], considerInputSlots))
            {
                return false;
            }
        }
        return true;
    }

    private Item RemoveSingleItem(Item item, bool considerInputSlots=false, bool fireEventOnRemove=true)
    {
        foreach (var slotObj in slots)
        {
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            if (slot.slotType == SlotType.OUTPUT || slot.slotType == SlotType.IN_OUT || considerInputSlots)
            {
                var temp = slot.AttemptRemove(item);
                if (temp != null)
                {
                    if (fireEventOnRemove)
                    {
                        GameEvents.current.InventoryChange(this.id);
                    }
                    return temp;
                }
            }
        }
        return null;
    }


    private List<Item> RemoveMultipleSingleItem(Item item, int quantityNeeded = 1, bool considerInputSlots = false, bool fireEventOnRemove = true)
    {
        // Note User should call has items before trying to remove any items
        List<Item> removed = new List<Item>();
        for (int i = 0; i < quantityNeeded; i++)
        {
            removed.Add(RemoveSingleItem(item, considerInputSlots, false));
        }

        if (fireEventOnRemove)
        {
            GameEvents.current.InventoryChange(this.id);
        }
        return removed;
    }

    public List<Item> RemoveMultipleItems(Dictionary<Item, int> inputItemQuantities, bool considerInputSlots = false, bool fireEventOnRemove = true)
    {
        List<Item> removed = new List<Item>();
        if (HasAllItems(inputItemQuantities, considerInputSlots))
        {
            foreach (Item item in inputItemQuantities.Keys)
            {
                removed.AddRange(RemoveMultipleSingleItem(item, inputItemQuantities[item], considerInputSlots, false));
            }
            if (fireEventOnRemove)
            {
                GameEvents.current.InventoryChange(this.id);
            }
        }
        return removed;
    }

    public void ForceClearSlots()
    {
        //Maybe will return something
        foreach (var slotObj in slots)
        {
            var slot = slotObj.GetComponent<InventorySlot>();
            slot.ForceClearSlot();
        }
    }


    // Lock Input Slots
    public void LockSlots(SlotType type)
    {
        foreach(var slotObj in slots)
        {
            var slot = slotObj.GetComponent<InventorySlot>();
            if(slot.slotType == type || type == SlotType.IN_OUT)
            {
                slot.locked = true;
            }
        }
    }

    public void UnlockSlots(SlotType type)
    {
        foreach (var slotObj in slots)
        {
            var slot = slotObj.GetComponent<InventorySlot>();
            if (slot.slotType == type || type == SlotType.IN_OUT)
            {
                slot.locked = false;
            }
        }
    }
}