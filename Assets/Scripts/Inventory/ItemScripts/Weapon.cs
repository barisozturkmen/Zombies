using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon: Equipment
{
    public WeaponType weaponType;
    public Gun gun;
    public float damage;
    public int ammo;

    public override void Use()
    {
        base.Use();
        gun.AssignWeapon(this);
    }

    public override Item Copy()
    {
        Weapon copiedItem = base.Copy() as Weapon;
        copiedItem.weaponType = this.weaponType;
        copiedItem.gun = this.gun;
        copiedItem.damage = this.damage;
        copiedItem.ammo = this.ammo;
        return copiedItem;
    }
}
