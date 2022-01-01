using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemStatus
{
    NA=-1, NOT_SET, RAW, COOKED, BURNT
}

public class Item : BaseGameObject
{
    public int buyPrice;
    public int sellPrice;
    public ItemStatus status = ItemStatus.NA;
    public int maxStackSize;

    public Item() { }

    public override string ToString()
    {
        return string.Format("[{0}-{1}]",id, objName);
    }
}
