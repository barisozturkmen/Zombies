using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum FireMode { Single, Burst, Auto };
public enum WeaponType { Pistol, Shotgun };
public enum AmmoType { NineMil, Shotgun }

public abstract class Gun : MonoBehaviour {

    public Transform[] projectileSpawn;
    public Projectile projectile;
    [NonSerialized] public int damage;
    [NonSerialized] public float range;
    [NonSerialized] public float shotTime;
    [NonSerialized] public float muzzleVelocity;
    [NonSerialized] public float reloadTime;
    [NonSerialized] public AmmoType ammoType;

    public Weapon weaponItem;
    public int magazineCapacity;
    public int ammoInMagazine = 0;

    public GameObject bullet;
    public Transform shell;
    public Transform shellEjector;
    public Transform spawn;


    public WeaponType weaponType;
    public FireMode fireMode;
    public int burstCount;
    public int shotsRemainingInBurst;

    public bool isReloading;
    public bool isShooting = false;

    public MuzzleFlash muzzleFlash;
    public AudioSource audioSource;
    public float nextPossibleShootTime;
    public bool triggerReleasedSinceLastShot = true;

    public LayerMask hitableLayerID;

    public virtual void Shoot()
    {
        if (CanShoot() == true)
        {
            if (fireMode == FireMode.Burst)
            {
                if (shotsRemainingInBurst == 0)
                {
                    return;
                }
                shotsRemainingInBurst--;
            }
            else if (fireMode == FireMode.Single)
            {
                if (!triggerReleasedSinceLastShot)
                {
                    return;
                }
            }
            for (int i = 0; i < projectileSpawn.Length; i++)
            {
                Projectile newProjectile = Instantiate(projectile, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectile;
                newProjectile.SetSpeed(muzzleVelocity);
                nextPossibleShootTime = Time.time + shotTime;
            }
            audioSource.Play();
            Instantiate(shell, shellEjector.position, shellEjector.rotation);
            muzzleFlash.Activate();
            isShooting = true;
        }
    }

    public virtual void ShootContinuous()
    {
        if (fireMode == FireMode.Auto)
        {
            Shoot();
        }
    }

    public virtual bool CanShoot()
    {
        bool canShoot = true;
        if (Time.time < nextPossibleShootTime)
        {
            canShoot = false;
        }
        return canShoot;
    }

    public virtual void OnTriggerHold()
    {
        Shoot();
        triggerReleasedSinceLastShot = false;
    }

    public virtual void OnTriggerRelease()
    {
        triggerReleasedSinceLastShot = true;
        shotsRemainingInBurst = burstCount;
    }

    public virtual bool CanReload()
    {
        if (ammoInMagazine == magazineCapacity)
        {
            return false;
        }
        foreach (Item item in Inventory.instance.items)
        {
            if (item is Ammo)
            {
                Ammo ammo = item as Ammo;
                if (ammo.ammoType == this.ammoType)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public virtual void Reload()
    {
        int ammoToReload = 0;
        Ammo ammo = null;
        foreach (Item item in Inventory.instance.items)
        {
            if (item is Ammo)
            {
                ammo = item as Ammo;
                if (ammo.ammoType == this.ammoType)
                {
                    if (ammo.quantity >= magazineCapacity - ammoInMagazine)
                    {
                        ammoToReload = magazineCapacity - ammoInMagazine;
                    }
                    else
                    {
                        ammoToReload = ammo.quantity;
                    }
                    ammo.quantity -= ammoToReload;
                    ammoInMagazine += ammoToReload;
                }
            }
        }
        if (ammo.quantity == 0)
        {
            Inventory.instance.DestroyItem(ammo);
        }
        else
        {
            Inventory.instance.UpdateUI();
        }
    }

    public virtual void AssignWeapon(Weapon weapon)
    {
        weaponItem = weapon;
    }
}
