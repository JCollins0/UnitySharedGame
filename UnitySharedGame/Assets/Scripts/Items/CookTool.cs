using UnityEngine;

[CreateAssetMenu(fileName = "New Cook Tool", menuName = "Items/CookTool")]
class CookTool : Item
{
    public override ItemType Type { get => ItemType.COOK_TOOL; }
    void Reset()
    {
        maxStackSize = 1;
        status = ItemStatus.NA;
    }
}
