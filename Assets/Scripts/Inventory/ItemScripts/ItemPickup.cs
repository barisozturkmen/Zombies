using UnityEngine;
using System.Collections;

public class ItemPickup: MonoBehaviour
{
    public string _name;
    public Sprite _icon;
    public bool _isDefaultItem;

    public virtual Item GetInstance()
    {
        return new Item
        {
            name = _name,
            icon = _icon,
            isDefaultItem = _isDefaultItem
        };
    }
    public virtual void ConfigurePickup(Item item)
    {
        Item droppedItem = item;
        _name = droppedItem.name;
        _icon = droppedItem.icon;
        _isDefaultItem = droppedItem.isDefaultItem;
    }
}