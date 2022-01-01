using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineCraftingRecipe : GameObject
{

    private Item inputItem;
    private int inputQuantity;
    
    private int minProcessingTime;
    private int maxProcessingTime;

    private Item outputItem;
    private int outputQuantity;
    
    public override string ToString()
    {
        return string.Format("[MCR-({6}): {0}x{1} -> [{4}-{5}] -> {2}x{3}]", inputItem, inputQuantity, outputItem, outputQuantity, minProcessingTime,maxProcessingTime, id);
    }
}