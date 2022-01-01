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

    public bool SimulateAdd(Item item, int id)
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
}
