using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPickup : ItemPickup {

    public EquipmentSlot _equipmentSlot;

    public override Item GetInstance()
    {

        return new Equipment
        {
            name = _name,
            icon = _icon,
            isDefaultItem = _isDefaultItem,
            equipmentSlot = _equipmentSlot
        };
    }

    public override void ConfigurePickup(Item item)
    {
        Equipment droppedItem = item as Equipment;
        _name = droppedItem.name;
        _icon = droppedItem.icon;
        _isDefaultItem = droppedItem.isDefaultItem;

        _equipmentSlot = droppedItem.equipmentSlot;
    }

}
