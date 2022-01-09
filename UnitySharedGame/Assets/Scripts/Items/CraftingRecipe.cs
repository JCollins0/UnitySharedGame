using System.Collections;
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

    public bool hasProcessingPenality;
    public int minProcessingTime;
    public int maxProcessingTime;

    public Item outputItem;
    public int outputQuantity;
    
    public override string ToString()
    {
        GetInputDictionary();
        return string.Format("[MCR-({6}): {0}x{1} -> [{4}-{7}-{5}] -> {2}x{3}]", inputItems.Keys, inputItems.Values, outputItem, outputQuantity, minProcessingTime,maxProcessingTime, id, hasProcessingPenality);
    }

    public string GetIngredientsText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("Makes:").AppendLine();
        builder.AppendFormat("{0}x {1}", outputQuantity, outputItem.itemName).AppendLine();
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
        Dictionary<Item, int> output = new Dictionary<Item, int>();
        if (hasProcessingPenality)
        {
            if(cookProgress < minProcessingTime)
            {
                output.Add(outputItem, outputQuantity);//todo change
                return output;
            }
            if(cookProgress > maxProcessingTime)
            {
                output.Add(outputItem, outputQuantity);
                return output; //todo change
            }
        }
        output.Add(outputItem, outputQuantity);
        return output;
    }
}