using UnityEngine;

public enum ItemStatus
{
    NA=-1, NOT_SET, RAW, COOKED, BURNT
}

public enum ItemType
{
    ITEM, FOOD
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int buyPrice;
    public int sellPrice;
    virtual public ItemStatus Status { get => ItemStatus.NA; }
    virtual public ItemType Type { get => ItemType.ITEM; }
    public int maxStackSize = 1;
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
