using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    // UI
    public Text quantityLabel;
    public Text itemNameLabel;
    public Image image;

    public InventorySlot slot;
    private Button myButtonComponent;
    
    // Start is called before the first frame update
    void Start()
    {
        quantityLabel.text = "";
        image = GetComponent<Image>();
        itemNameLabel.text = "";
        myButtonComponent = this.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFromSlot();

        myButtonComponent.interactable = slot.Interactable();
    }

    void UpdateFromSlot()
    {
        if (slot)
        {
            if (slot.heldItem)
            {
                image.sprite = slot.heldItem.sprite;
                itemNameLabel.text = slot.heldItem.itemName;
                quantityLabel.text = slot.quantity.ToString();
            }
            else if (image.sprite != null)
            {
                image.sprite = null;
                itemNameLabel.text = "";
                quantityLabel.text = "";
            }
        }
        else
        {
            Debug.LogError("Slot is not defined");
        }
    }
}
