using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EquipmentSlot { Head, Chest, Back, Legs, Feet, Weapon }

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    //public int armorModifier;
    //public int damageModifier;
    public EquipmentSlot equipmentSlot;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
    }
}
