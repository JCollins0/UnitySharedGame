using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipes/CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    public int id;
    public string recipeName;
    public List<Item> inputItemsList;
    private Dictionary<Item, int> inputItems;

    public bool hasProcessingPenality;
    public int minProcessingTime;
    public int maxProcessingTime;

    public List<Item> outputItemsList;
    public List<Item> underCookedItemsList;
    public List<Item> overCookedItemsList;
    private Dictionary<ItemStatus, Dictionary<Item, int>> outputItems;

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

    public Dictionary<ItemStatus, Dictionary<Item, int>> GetOutputDictionary()
    {
        if(outputItems == null)
        {
            outputItems = new Dictionary<ItemStatus, Dictionary<Item, int>>();

            outputItems[ItemStatus.COOKED] = FillOutputItemDictionary(outputItemsList, new Dictionary<Item, int>());
            if (hasProcessingPenality)
            {
                outputItems[ItemStatus.RAW] = FillOutputItemDictionary(underCookedItemsList, new Dictionary<Item, int>());
                outputItems[ItemStatus.BURNT] = FillOutputItemDictionary(overCookedItemsList, new Dictionary<Item, int>());
            }
        }

        return outputItems;
    }

    private Dictionary<Item, int> FillOutputItemDictionary(List<Item> items, Dictionary<Item, int> outputDict)
    {
        foreach (var item in items)
        {
            if (!outputDict.ContainsKey(item))
            {
                outputDict[item] = 0;
            }
            outputDict[item]++;
        }
        return outputDict;
    }

    void Start()
    {
    }

    public override string ToString()
    {
        GetInputDictionary();
        GetOutputDictionary();
        return string.Format("[MCR-({6}): {0}x{1} -> [{4}-{7}-{5}] -> {2}x{3}]", inputItems.Keys, inputItems.Values, outputItems[ItemStatus.COOKED].Keys, outputItems[ItemStatus.COOKED].Values, minProcessingTime,maxProcessingTime, id, hasProcessingPenality);
    }

    public string GetIngredientsText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("Makes:").AppendLine();

        foreach(Item outputItem in GetOutputDictionary()[ItemStatus.COOKED].Keys)
        {
            int outputQuantity = outputItems[ItemStatus.COOKED][outputItem];
            builder.AppendFormat("{0}x {1}", outputQuantity, outputItem.itemName).AppendLine();
        }

        builder.AppendLine();
        builder.Append("Using:").AppendLine();
        foreach (Item item in GetInputDictionary().Keys)
        {
            builder.AppendFormat("{0}x {1}", inputItems[item], item.itemName).AppendLine();
        }
        return builder.ToString();
    }

    public Dictionary<Item,int> GetRecipeOutputBasedOnCookTime(int cookProgress)
    {
        if (hasProcessingPenality)
        {
            if(cookProgress < minProcessingTime)
            {
                return outputItems[ItemStatus.RAW];
            }
            if(cookProgress > maxProcessingTime)
            {
                return outputItems[ItemStatus.BURNT];
            }
        }

        return outputItems[ItemStatus.COOKED];
    }
}