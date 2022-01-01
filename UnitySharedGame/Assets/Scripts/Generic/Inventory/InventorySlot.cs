using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType
{
    INPUT=1, OUTPUT, IN_OUT
}

public class InventorySlot : MonoBehaviour
{

    public GameObject heldItem;
    public int quantity;
    public int maxStackSize;
    public SlotType slotType = SlotType.IN_OUT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
