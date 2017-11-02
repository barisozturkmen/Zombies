using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GunShotgun: Gun
{
    public GunShotgun()
    {
        range = 15f;
        shotTime = 0.6f;
        muzzleVelocity = 150f;
        reloadTime = 1f;
        weaponType = WeaponType.Shotgun;
        fireMode = FireMode.Single;
        burstCount = 0;
        shotsRemainingInBurst = burstCount;
        ammoType = AmmoType.Shotgun;
        magazineCapacity = 5;
    }

    public int pelletCount = 50;
    public float spray = 200f;
    public float pumpTime = 0.6f;
    public float loadShellTime = 0.6f;
    public float reloadEndTime = 0.6f;
    public bool stopReload = false;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        muzzleFlash = this.GetComponent<MuzzleFlash>();
    }

    public override void Shoot()
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

            for (int i = 0; i < pelletCount; i++)
            {
                float pelletAngle = UnityEngine.Random.Range(-spray, spray);
                RaycastHit hit;
                Vector3 forward = this.transform.TransformDirection(Vector3.forward);
                forward = Quaternion.Euler(0, pelletAngle, 0) * forward;
                Quaternion rotation = Quaternion.Euler(0, pelletAngle, 0) * this.transform.rotation;


                //Vector3 projectileAngle = new Vector3(0, UnityEngine.Random.Range(-spray, spray), 0);
                Projectile newProjectile = Instantiate(projectile, spawn.position, rotation) as Projectile;
                newProjectile.transform.Rotate(forward);
                newProjectile.SetSpeed(muzzleVelocity);

                if (Physics.Raycast(transform.position, forward, out hit, range, hitableLayerID))
                {
                    Debug.Log("HIT ZOMBIE!!");

                }
                //Debug.DrawRay(transform.position, forward * 100f, Color.blue);


                nextPossibleShootTime = Time.time + shotTime;
            }

            //for (int i = 0; i < pelletCount; i++)
            //{
            //    //Vector3 pelletAngle = new Vector3(0, UnityEngine.Random.Range(-30, 30), 0);
            //    float pelletAngle = UnityEngine.Random.Range(-spray, spray);
            //    RaycastHit hit;
            //    Vector3 forward = this.transform.TransformDirection(Vector3.forward);
            //    //forward = Quaternion.AngleAxis(pelletAngle, Vector3.up) * forward;
            //    forward = Quaternion.Euler(0, pelletAngle, 0) * forward;
            //
            //    if (Physics.Raycast(transform.position, forward, out hit, range, hitableLayerID))
            //    {
            //        Debug.Log("HIT ZOMBIE!!");
            //
            //    }
            //    Debug.DrawRay(transform.position, forward * 100f, Color.blue);
            //}

            ammoInMagazine -= 1;
            Inventory.instance.onItemChangedCallback.Invoke();
            weaponItem.ammo = ammoInMagazine;
            audioSource.Play();
            Instantiate(shell, shellEjector.position, shellEjector.rotation);
            muzzleFlash.Activate();
            isShooting = true;
        }
    }

    public override void ShootContinuous()
    {
        if (fireMode == FireMode.Auto)
        {
            Shoot();
        }
    }

    public override bool CanShoot()
    {
        bool canShoot = true;
        if (Time.time < nextPossibleShootTime || (ammoInMagazine <= 0))
        {
            canShoot = false;
        }
        return canShoot;
    }

    public override void OnTriggerHold()
    {
        Shoot();
        triggerReleasedSinceLastShot = false;
    }

    public override void OnTriggerRelease()
    {
        triggerReleasedSinceLastShot = true;
        shotsRemainingInBurst = burstCount;
    }


    public override void Reload()
    {
        Debug.Log("reload counter");
        int ammoToReload = 0;
        Ammo ammo = null;
        foreach (Item item in Inventory.instance.items)
        {
            if (item is Ammo)
            {
                ammo = item as Ammo;
                if (ammo.ammoType == this.ammoType)
                {
                    if (ammo.quantity > 0 && 
                        ammoInMagazine < magazineCapacity)
                    {
                        ammoToReload = 1;
                    }
                    else
                    {
                        ammoToReload = 0;
                        stopReload = true;
                        //stop coroutines, skip to end reload animation
                        Debug.Log("stopReload bool set to true");
                    }
                    ammo.quantity -= ammoToReload;
                    ammoInMagazine += ammoToReload;
                }
            }
        }
        if (ammo.quantity == 0)
        {
            stopReload = true;
            Inventory.instance.DestroyItem(ammo);
        }
        else
        {
            Inventory.instance.UpdateUI();
        }
    }



}
