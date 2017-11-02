using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : EquipmentPickup
{

    public WeaponType _weaponType;
    public Gun _gun;
    public float _damage;
    public int _ammo;

    public override Item GetInstance()
    {

        //WeaponPickup stackable = base.GetInstance() as WeaponPickup;
        return new Weapon
        {
            name = _name,
            icon = _icon,
            isDefaultItem = _isDefaultItem,
            equipmentSlot = _equipmentSlot,
            weaponType = _weaponType,
            gun = _gun,
            damage = _damage,
            ammo = _ammo
        };
    }

    public override void ConfigurePickup(Item item)
    {
        Weapon droppedItem = item as Weapon;
        _name = droppedItem.name;
        _icon = droppedItem.icon;
        _isDefaultItem = droppedItem.isDefaultItem;

        _equipmentSlot = droppedItem.equipmentSlot;

        _weaponType = droppedItem.weaponType;
        _gun = droppedItem.gun;
        _damage = droppedItem.damage;
        _ammo = droppedItem.ammo;


    }
}
