using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemSo : ScriptableObject
{
    public Sprite itemIcon;
    public string itemID;
    public string itemName;
}
