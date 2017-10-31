using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class GunPistol : Gun {

    public GunPistol()
    {
        range = 100f;
        shotTime = 0.6f;
        muzzleVelocity = 150f;
        reloadTime = 1f;
        weaponType = WeaponType.Pistol;
        fireMode = FireMode.Single;
        burstCount = 3;
        shotsRemainingInBurst = burstCount;
        ammoType = AmmoType.NineMil;
        magazineCapacity = 15;
    }

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
            for (int i = 0; i < projectileSpawn.Length; i++)
            {
                Projectile newProjectile = Instantiate(projectile, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectile;
                newProjectile.SetSpeed(muzzleVelocity);
                nextPossibleShootTime = Time.time + shotTime;
            }

            RaycastHit hit;
            Vector3 forward = this.transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, forward, out hit, range, hitableLayerID))
            {
                Debug.Log("HIT ZOMBIE!!");
            }
            ammoInMagazine -= 1;
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
        if ((Time.time < nextPossibleShootTime) || (ammoInMagazine <= 0))
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
}
