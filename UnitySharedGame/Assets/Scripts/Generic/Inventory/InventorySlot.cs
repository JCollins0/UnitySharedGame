using System;
using UnityEngine;

public enum SlotType
{
    INPUT = 1, OUTPUT, IN_OUT
}


[CreateAssetMenu(fileName = "New Inventory Slot", menuName = "Inventory/Slot")]
public class InventorySlot : ScriptableObject
{
    // Logic
    public Item heldItem;
    public int quantity;
    public int maxStackSize = 1;
    public SlotType slotType = SlotType.IN_OUT;
    public bool locked;

    private static readonly Tuple<bool, int> EMPTY_TUPLE = Tuple.Create(false, 0);
    private static readonly Tuple<Item, int> EMPTY_ITEM_TUPLE = Tuple.Create((Item)null, 0);

    public bool Interactable()
    {
        return slotType == SlotType.OUTPUT && heldItem != null;
    }
    
    public Tuple<Item, int> PeekOutput()
    {
        if (heldItem == null)
        {
            return EMPTY_ITEM_TUPLE;
        }

        return new Tuple<Item, int>(heldItem, quantity);
    }

    public Tuple<Item, int> ForceClearSlot()
    {
        if (heldItem == null)
        {
            return EMPTY_ITEM_TUPLE;
        }

        int tempQuantity = quantity;
        Item temp = heldItem;

        quantity = 0;
        heldItem = null;

        return new Tuple<Item, int>(temp, tempQuantity);
    }

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

    public Item AttemptRemove(Item item)
    {
        if (locked)
        {
            return null;
        }

        if (heldItem == null)
        {
            return null;
        }

        if (heldItem.id == item.id && quantity >= 1)
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
        if (locked)
        {
            return false;
        }

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

    public Tuple<bool, int> CanAdd(Item item, int quantityNeeded = 1, bool considerOutputSlots = false)
    {
        if (locked)
        {
            return EMPTY_TUPLE;
        }
        // Cannot add if slot is output
        if (slotType == SlotType.OUTPUT)
        {
            if (!considerOutputSlots)
            {
                return EMPTY_TUPLE;
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

        return EMPTY_TUPLE;
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
        if (locked)
        {
            return false;
        }
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
        if (locked)
        {
            return false;
        }
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
