using UnityEngine;

public enum ItemStatus
{
    NA=-1, NOT_SET, RAW, PROCESSED, COOKED, BURNT
}

public enum ItemType
{
    ITEM, FOOD, COOK_TOOL
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int buyPrice;
    public int sellPrice;
    public ItemStatus status = ItemStatus.NA;
    virtual public ItemType Type { get => ItemType.ITEM; }
    public int maxStackSize;
    public Sprite sprite;
    
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
        return string.Format("[{0}-{1}]",id, itemName);
    }


}
