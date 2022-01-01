using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class CraftingRecipe : BaseGameObject
{
    public List<Item> inputItemsList;
    private Dictionary<Item, int> inputItems;

    public Dictionary<Item, int> GetInputDictionary()
    {
        if (inputItems == null)
        {
            inputItems = new Dictionary<Item, int>();

            foreach (var item in inputItemsList)
            {
                if (!inputItems.ContainsKey(item))
                {
                    inputItems[item] = 0;
                }
                inputItems[item]++;
            }
        }

        return inputItems;
    }

    void Start()
    {
    }

    public int minProcessingTime;
    public int maxProcessingTime;

    public Item outputItem;
    public int outputQuantity;
    
    public override string ToString()
    {
        GetInputDictionary();
        return string.Format("[MCR-({6}): {0}x{1} -> [{4}-{5}] -> {2}x{3}]", inputItems.Keys, inputItems.Values, outputItem, outputQuantity, minProcessingTime,maxProcessingTime, id);
    }

    public string GetIngredientsText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("Makes:").AppendLine();
        builder.AppendFormat("{0}x {1}", outputQuantity, outputItem.objName).AppendLine();
        builder.AppendLine();
        builder.Append("Using:").AppendLine();
        foreach (Item item in GetInputDictionary().Keys)
        {
            builder.AppendFormat("{0}x {1}", inputItems[item], item.objName).AppendLine();
        }
        return builder.ToString();
    }
}