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

    public Item AttemptRemove(int id)
    {
        if(heldItem == null)
        {
            return null;
        }

        if (heldItem.id == id && quantity >= 1)
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
