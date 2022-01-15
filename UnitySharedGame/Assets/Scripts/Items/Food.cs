using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Items/Food")]
public class Food : Item
{
    public override ItemType Type { get => ItemType.FOOD; }

    void Reset()
    {
        maxStackSize = 1;
        status = ItemStatus.NOT_SET;
    }
}
