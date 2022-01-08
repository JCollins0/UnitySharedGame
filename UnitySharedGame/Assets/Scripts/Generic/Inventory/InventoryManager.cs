using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : BaseGameObject
{
    public List<GameObject> slots;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < slots.Count; i++)
        {
            slots[i].transform.SetParent(this.transform);
            slots[i].GetComponent<Button>().onClick = ClickInventorySlot(slots[i]);
        }
    }

    public Button.ButtonClickedEvent ClickInventorySlot(GameObject slotObj)
    {
        Button.ButtonClickedEvent b = new Button.ButtonClickedEvent();
        b.AddListener(() =>{
            GameEvents.current.InventorySlotClick(slotObj.GetComponent<InventorySlot>());
        });
        return b;
    }

    public bool SimulateAddItems(Dictionary<Item, int> inputItemQuantities)
    {
        // Need to Simulate Entire Transaction

        List<InventorySlotScriptable> transaction = new List<InventorySlotScriptable>();
        slots.ForEach((slotObj) =>
        {
            var slot = slotObj.GetComponent<InventorySlot>();
            if(slot.slotType == SlotType.INPUT || slot.slotType == SlotType.IN_OUT)
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
            bool canAdd = false;
            foreach(var slot in transaction)
            {
                if (slot.CanAdd(item.id))
                {
                    canAdd = true;
                    if(!slot.SimulateAdd(item))
                    {
                        return false;
                    }
                }
            }
            if (!canAdd)
            {
                return false;
            }
        }
        
        return true;
    }


    public bool CanAddItem(int id, int quantityNeeded=1)
    {

        foreach(var slotObj in slots)
        {
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            if(slot != null && (slot.slotType == SlotType.INPUT || slot.slotType == SlotType.IN_OUT ))
            {
                if (slot.CanAdd(id))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool AddItem(Item item)
    {
        foreach (var slotObj in slots)
        {
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            if (slot != null && (slot.slotType == SlotType.INPUT || slot.slotType == SlotType.IN_OUT))
            {
                if (slot.AttemptAdd(item))
                {
                    GameEvents.current.InventoryChange(this.id);
                    return true;
                }
            }
        }
        return false;

    }

    public bool HasAllItems(Dictionary<Item, int> inputItemQuantities)
    {
        foreach (var item in inputItemQuantities.Keys)
        {
            if (!HasItem(item.id, inputItemQuantities[item]))
            {
                return false;
            }
        }
        return true;
    }

            

    public bool HasItem(int id, int quantityNeeded = 1)
    {
        Dictionary<int, int> amountLookup = new Dictionary<int, int>();
        amountLookup[id] = 0;

        foreach (var slotObj in slots)
        {
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            if (slot != null && (slot.slotType == SlotType.OUTPUT || slot.slotType == SlotType.IN_OUT))
            {
                amountLookup[id] += slot.HasItem(id);
            }
        }

        if(amountLookup[id] >= quantityNeeded)
        {
            return true;
        }


        return false;
    }


    public List<Item> RemoveItems(Item item, int quantityNeeded = 1)
    {
        List<Item> removed = new List<Item>();
        for(int i = 0; i < quantityNeeded; i++)
        {
            removed.Add(RemoveSingleItem(item));
        }
        return removed;
    }


    public Item RemoveSingleItem(Item item)
    {
        foreach (var slotObj in slots)
        {
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            if (slot != null && (slot.slotType == SlotType.OUTPUT || slot.slotType == SlotType.IN_OUT))
            {
                var temp = slot.AttemptRemove(item);
                if(temp != null)
                {
                    GameEvents.current.InventoryChange(this.id);
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
