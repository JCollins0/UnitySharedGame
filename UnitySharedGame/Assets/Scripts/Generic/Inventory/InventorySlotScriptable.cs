using System;
using UnityEngine;


public class InventorySlotScriptable
{
    // Logic
    public Item heldItem;
    public int quantity;
    public int maxStackSize;
    public SlotType slotType = SlotType.IN_OUT;

    public int HasItem(int id)
    {
        if (heldItem == null)
        {
            return 0;
        }

        if (heldItem.id == id)
        {
            return quantity;
        }
        return 0;

    }

    public Item AttemptRemove(int id)
    {
        if (heldItem == null)
        {
            return null;
        }

        if (heldItem.id == id && quantity >= 1)
        {
            --quantity;
            
            var temp = heldItem;
            if (quantity == 0)
            {
                heldItem = null;
            }
            return temp;
        }

        return null;
    }

    public bool CanAdd(int id)
    {
        // Case 1: Empty Slot
        if (heldItem == null)
        {
            return true;
        }

        // Case 2: Slot is filled but id matches and item's max stack size is not reached
        if (heldItem.id == id && quantity < maxStackSize && quantity < heldItem.maxStackSize)
        {
            return true;
        }

        return false;
    }

    public bool AttemptAdd(Item item, int id)
    {
        // Case 1: Empty Slot
        if (heldItem == null)
        {
            heldItem = item;
            quantity = 1;
            return true;
        }

        // Case 2: Slot is filled but id matches and item's max stack size is not reached

        if (heldItem.id == id && quantity < maxStackSize && quantity < heldItem.maxStackSize)
        {
            quantity++;
            return true;
        }
        return false;
    }

    public Tuple<bool, int> CanAdd(Item item, int quantityNeeded = 1, bool considerOutputSlots = false)
    {
        // Cannot add if slot is output
        if (slotType == SlotType.OUTPUT)
        {
            if (!considerOutputSlots)
            {
                return new Tuple<bool, int>(false, 0);
            }
        }

        if (heldItem == null)
        {
            return new Tuple<bool, int>(true, Math.Min(item.maxStackSize, maxStackSize));
        }

        if (heldItem.id == item.id && quantity < maxStackSize && quantity < heldItem.maxStackSize)
        {
            int maxAmountOfItemsCanAdd = Math.Min(item.maxStackSize, maxStackSize) - quantity;
            return new Tuple<bool, int>(true, Math.Max(quantityNeeded - maxAmountOfItemsCanAdd, 0));
        }

        return new Tuple<bool, int>(false, 0);
    }

    public int HasItem(Item item, bool considerInputSlots = false)
    {
        if (slotType == SlotType.INPUT)
        {
            if (!considerInputSlots)
            {
                return 0;
            }
        }

        if (heldItem == null)
        {
            return 0;
        }

        if (heldItem.id == item.id)
        {
            return quantity;
        }
        return 0;
    }

    public bool SimulateAdd(Item item)
    {
        // Case 1: Empty Slot
        if (heldItem == null)
        {
            heldItem = item;
            quantity = 1;
            return true;
        }


        // Case 2: Slot is filled but id matches and item's max stack size is not reached
        if (heldItem.id == item.id && quantity < maxStackSize && quantity < heldItem.maxStackSize)
        {
            quantity++;
            return true;
        }
        return false;
    }

    public bool AttemptAdd(Item item)
    {
        // Case 1: Empty Slot
        if (heldItem == null)
        {
            heldItem = item;
            quantity = 1;
            return true;
        }

        // Case 2: Slot is filled but id matches and item's max stack size is not reached

        if (heldItem.id == item.id && quantity < maxStackSize && quantity < heldItem.maxStackSize)
        {
            quantity++;
            return true;
        }
        return false;
    }
}
