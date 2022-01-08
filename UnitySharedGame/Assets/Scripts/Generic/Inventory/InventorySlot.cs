using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SlotType
{
    INPUT=1, OUTPUT, IN_OUT
}

public class InventorySlot : MonoBehaviour
{
    // UI
    public Text quantityLabel;
    public Text itemNameLabel;
    public Image image;

    // Logic
    public Item heldItem;
    public int quantity;
    public int maxStackSize;
    public SlotType slotType = SlotType.IN_OUT;
    public bool locked;

    private static Tuple<bool, int> EMPTY_TUPLE = Tuple.Create(false, 0);
    private static Tuple<Item, int> EMPTY_ITEM_TUPLE = Tuple.Create((Item)null, 0);


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
        if(heldItem == null)
        {
            return EMPTY_ITEM_TUPLE;
        }

        int tempQuantity = quantity;
        Item temp = heldItem;

        quantity = 0;
        quantityLabel.text = "";
        itemNameLabel.text = "";
        image.sprite = null;
        heldItem = null;

        return new Tuple<Item, int>(temp, tempQuantity);
    }

    public int HasItem(int id)
    {
        if(heldItem == null)
        {
            return 0;
        }
        
        if(heldItem.id == id)
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

        if(heldItem == null)
        {
            return null;
        }

        if (heldItem.id == item.id && quantity >= 1)
        {
            --quantity;
            quantityLabel.text = quantity.ToString();

            var temp = heldItem;
            if (quantity == 0)
            {
                quantityLabel.text = "";
                itemNameLabel.text = "";
                image.sprite = null;
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

    public Tuple<bool, int> CanAdd(Item item, int quantityNeeded = 1, bool considerOutputSlots=false)
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

        if( heldItem == null)
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
            quantityLabel.text = quantity.ToString();
            itemNameLabel.text = heldItem.objName;
            image.sprite = item.sprite;
            return true;
        }

        // Case 2: Slot is filled but id matches and item's max stack size is not reached
        
        if (heldItem.id == item.id && quantity < maxStackSize && quantity < heldItem.maxStackSize)
        {
            quantity++;
            quantityLabel.text = quantity.ToString();
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        quantityLabel.text = "";
        image = GetComponent<Image>();
        itemNameLabel.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
