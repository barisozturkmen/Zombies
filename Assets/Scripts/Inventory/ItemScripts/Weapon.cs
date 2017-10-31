using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment {

    public WeaponType weaponType;
    public Gun gun;
    public float damage;
    public int ammo;

    public int GetAmmoRemaining()
    {
        return gun.ammoInMagazine;
    }

}
