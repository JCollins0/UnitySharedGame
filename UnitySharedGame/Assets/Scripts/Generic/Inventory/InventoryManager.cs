using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    /*

        TODO: Currently ivnetory system doesn't have the ability
        to have consider multiple stacks of the same item type...
        unless you remove them one at a time...

    */


    public int maxSlots;
    public GameObject slotPrefab;
    private List<GameObject> slots;

    // Start is called before the first frame update
    void Start()
    {
        slots = new List<GameObject>();
        for(int i = 0; i < maxSlots; i++)
        {
            var slot = Instantiate(slotPrefab);
            slot.transform.SetParent(this.transform);
            slots.Add(slot);
        }
    }


    public bool CanAddItem(GameObject obj)
    {
        Item item = obj.GetComponent<Item>();

        if(item == null)
        {
            throw new System.Exception("Object being added is not an item....");
        }

        int id = item.id;

        foreach(var slotObj in slots)
        {
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            if(slot != null && (slot.slotType == SlotType.INPUT || slot.slotType == SlotType.IN_OUT ))
            {
                // Case 1: Empty Slot
                if (slot.heldItem == null)
                {
                    return true;
                }

                // Case 2: Slot is filled but id matches and item's max stack size is not reached
                Item slotItem = slot.heldItem.GetComponent<Item>();
                if (slotItem.id == id && slot.quantity < slotItem.maxStackSize)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void AddItem(GameObject obj)
    {
        Item item = obj.GetComponent<Item>();

        if (item == null)
        {
            throw new System.Exception("Object being added is not an item....");
        }

        int id = item.id;

        foreach (var slotObj in slots)
        {
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            if (slot != null && (slot.slotType == SlotType.INPUT || slot.slotType == SlotType.IN_OUT))
            {
                // Case 1: Empty Slot
                if (slot.heldItem == null)
                {
                    slot.heldItem = obj;
                    slot.quantity = 1;
                    break;
                }

                // Case 2: Slot is filled but id matches and item's max stack size is not reached
                Item slotItem = slot.heldItem.GetComponent<Item>();
                if (slotItem.id == id && slot.quantity < slotItem.maxStackSize)
                {
                    slot.quantity++;
                    Destroy(obj);
                    break;
                }
            }
        }

    }

    public bool HasItem(GameObject obj, int quantityNeeded = 1)
    {
        Item item = obj.GetComponent<Item>();

        if (item == null)
        {
            throw new System.Exception("Object being tested is not an item....");
        }

        int id = item.id;

        foreach (var slotObj in slots)
        {
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            if (slot != null && (slot.slotType == SlotType.OUTPUT || slot.slotType == SlotType.IN_OUT) && slot.heldItem != null)
            {
                Item slotItem = slot.heldItem.GetComponent<Item>();
                if (slotItem.id == id && slot.quantity >= quantityNeeded)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public GameObject RemoveItem(GameObject obj, int quantityNeeded=1)
    {
        Item item = obj.GetComponent<Item>();

        if (item == null)
        {
            throw new System.Exception("Object being removed is not an item....");
        }

        int id = item.id;

        foreach (var slotObj in slots)
        {
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            if (slot != null && (slot.slotType == SlotType.OUTPUT || slot.slotType == SlotType.IN_OUT) && slot.heldItem != null)
            {
                Item slotItem = slot.heldItem.GetComponent<Item>();
                if (slotItem.id == id && slot.quantity >= quantityNeeded)
                {
                    var temp = slot.heldItem;
                    slot.quantity -= quantityNeeded;
                    if(slot.quantity == 0)
                    {
                        slot.heldItem = null;
                    }
                    return temp;
                }
            }
        }
        return null;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
