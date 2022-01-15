using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item Registry", menuName = "Item Registry")]
public class ItemRegistry : ScriptableObject
{
    public List<Item> itemRegistry;


    public ItemRegistry()
    {
        CheckForConflictingIds();
    }

    private void CheckForConflictingIds()
    {
        Dictionary<int, int> counts = new Dictionary<int, int>();
        foreach(var item in itemRegistry)
        {
            var itemId = item.id;
            if (!counts.ContainsKey(itemId))
            {
                counts.Add(itemId, 0);
            }
            counts[itemId]++;
        }

        foreach(var key in counts.Keys)
        {
            var val = counts[key];
            if( val > 1)
            {
                throw new System.Exception(string.Format("Conflicting item ids for id {0}", key));
            }
        }
    }
}
