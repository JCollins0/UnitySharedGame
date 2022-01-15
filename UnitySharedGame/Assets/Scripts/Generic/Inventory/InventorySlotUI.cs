using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    // UI
    public Text quantityLabel;
    public Tooltip tooltip;
    public Image image;

    public InventorySlot slot;
    private Button myButtonComponent;
    public RectTransform layout;
    
    // Start is called before the first frame update
    void Start()
    {
        quantityLabel.text = "";
        image = GetComponent<Image>();
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
            if (slot.heldItem != null)
            {
                image.sprite = slot.heldItem.sprite;
                tooltip.message = slot.heldItem.itemName;
                quantityLabel.text = slot.quantity.ToString();
                layout.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,quantityLabel.preferredWidth + 8f);
            }
            else
            {
                image.sprite = null;
                tooltip.message = null;
                quantityLabel.text = "";
                layout.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            }
        }
        else
        {
            Debug.LogError("Slot is not defined");
        }
    }
}
