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
    public Sprite sprite;

    public Item() { }

    public override bool Equals(object other)
    {
        if (other is Item)
        {
            return id.Equals(((Item)other).id);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return id;
    }

    public override string ToString()
    {
        return string.Format("[{0}-{1}]",id, objName);
    }


}
