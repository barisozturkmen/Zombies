using UnityEngine;
using UnityEditor;
using System;


public class AmmoPickup: StackablePickup
{
    [SerializeField]
    AmmoType _ammoType;

    public override Item GetInstance()
    {
        return new Ammo
        {
            name = _name,
            icon = _icon,
            isDefaultItem = _isDefaultItem,
            quantity = _quantity,
            ammoType = _ammoType
        };        
    }

    public override void ConfigurePickup(Item item)
    {
        Ammo droppedItem = item as Ammo;
        _name = droppedItem.name;
        _icon = droppedItem.icon;
        _isDefaultItem = droppedItem.isDefaultItem;

        _quantity = droppedItem.quantity;

        _ammoType = droppedItem.ammoType;
    }
}