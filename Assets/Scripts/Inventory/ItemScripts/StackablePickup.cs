using UnityEngine;
using UnityEditor;
using System;


public class StackablePickup: ItemPickup
{

    public int _quantity;

    public override Item GetInstance()
    {

        //Stackable stackable = base.GetInstance() as Stackable;
        return new Stackable
        {
            name = _name,
            icon = _icon,
            isDefaultItem = _isDefaultItem,
            quantity = _quantity
        };
    }

    public override void ConfigurePickup(Item item)
    {
        Stackable droppedItem = item as Stackable;
        _name = droppedItem.name;
        _icon = droppedItem.icon;
        _isDefaultItem = droppedItem.isDefaultItem;

        _quantity = droppedItem.quantity;
    }
}